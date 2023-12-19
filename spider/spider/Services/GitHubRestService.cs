using spider.Dtos;
using RestSharp;
using RestSharp.Serializers.Json;

namespace spider.Services;

public class GitHubRestService : IGitHubRestService
{
    private readonly IRestClient _gitHubRestClient;
    private readonly ILogger<GitHubRestService> _logger;
    private readonly SystemTextJsonSerializer _jsonSerializer;
    public GitHubRestService(IRestClient gitHubRestClient)
    {
        _gitHubRestClient = gitHubRestClient;
        _logger = new Logger<GitHubRestService>(new LoggerFactory());
        _jsonSerializer = new SystemTextJsonSerializer();
    }

    
    /// <summary>
    /// GetRepoContributors sends a rest request to the github api and returns on success and otherwise handles the
    /// error and retries if necessary.
    /// </summary>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <param name="repoName">Name of the repository</param>
    /// <param name="amount">amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?</returns>
    public async Task<List<ContributorDto>?> GetRepoContributors(string ownerName, string repoName, int amount = 50)
    {
        var result = new List<ContributorDto>();
        var request = new RestRequest("repos/" + ownerName + "/" + repoName + "/contributors");
        request.AddQueryParameter("per_page", 50);
        int page = 1;
        while (amount > 0)
        {
            if (amount > 50)
            {
                request.AddQueryParameter("page", page);
                try
                {
                    var restResponse = await _gitHubRestClient.ExecuteAsync(request).ConfigureAwait(false);
                    if (restResponse.IsSuccessful)
                    {
                        if (restResponse.Content == null || restResponse.ContentLength == 0)
                        {
                            return result;
                        }
                        
                        List<ContributorDto> restResult =
                            _jsonSerializer.Deserializer.Deserialize<List<ContributorDto>>(restResponse);

                        result.AddRange(restResult);
                        if (restResult.Count < 50)
                        {
                            break;
                        }
                    }
                    else
                    {
                        HandleError(restResponse);
                    }
                }
                catch (Exception e)
                {
                     _logger.LogError(e.Message + " in {origin} with request: \"{ownerName}/{repoName}\"",
                         this, ownerName,repoName);
                    throw;
                }

                page++;
                amount -= 50;
            }
            else
            {
                request.AddQueryParameter("page", page);
                try
                {
                    var temp = await _gitHubRestClient.ExecuteAsync(request).ConfigureAwait(false);
                    if (temp.IsSuccessful)
                    {
                        if (temp.Content == null || temp.ContentLength == 0)
                        {
                            return result;
                        }
                        
                        List<ContributorDto> restResult =
                            _jsonSerializer.Deserializer.Deserialize<List<ContributorDto>>(temp);

                        if (restResult.Count < amount)
                        {
                            result.AddRange(restResult);
                            break;
                        }

                        result.AddRange(restResult.GetRange(0, amount));
                    }
                    else
                    {
                        HandleError(temp);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message + " in {origin} with request: \"{ownerName}/{repoName}\"",
                        this, ownerName,repoName);
                    throw;
                }
                amount = 0;
            }
        }

        return result;
    }

    
    /// <summary>
    /// HandleErrors checks if there is a rate-limit error and if there is, it retries
    /// </summary>
    /// <param name="restResponse">The restResponse that includes the necessary headers</param>
    private void HandleError(RestResponse restResponse)
    {
        var header = restResponse.Headers.FirstOrDefault(x => x.Name == "X-RateLimit-Remaining");
        if (Convert.ToInt32(header.Value) == 0)
        {
            header = restResponse.Headers.FirstOrDefault(x => x.Name == "X-RateLimit-Reset");
            
            DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(header.Value));
            DateTime retryTime = utcTime.DateTime;
            Thread.Sleep(TimeSpan.FromSeconds((int)(retryTime - DateTime.UtcNow).TotalSeconds));
            _logger.LogWarning("Rate limit reached. Retrying in {seconds} seconds", (int)(retryTime - DateTime.UtcNow).TotalSeconds);
            return;
        }

        header = restResponse.Headers.FirstOrDefault(x => x.Name == "Retry-After");
        if (header is not null)
        {
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(header.Value.ToString())));
            _logger.LogWarning("Rate limit reached. Retrying in {seconds} seconds", header.Value);
            return;
        }

        restResponse.ThrowIfError();
    }
}