using System.Text.Json;
using spider.Dtos;
using RestSharp;

namespace spider.Services;

/// <summary>
/// GitHubRestService is a service that handles the communication with the github rest api
/// </summary>
public class GitHubRestService : IGitHubRestService
{
    private readonly IRestClient _gitHubRestClient;
    private readonly ILogger<GitHubRestService> _logger;
    private readonly JsonSerializerOptions _deserializerOptions;
    
    public GitHubRestService(IRestClient gitHubRestClient)
    {
        _gitHubRestClient = gitHubRestClient;
        _logger = new Logger<GitHubRestService>(new LoggerFactory());
        
        // Set the deserializer options to expect snake_case in order to be able to parse the node_id property of the contributors
        _deserializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
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
                            JsonSerializer.Deserialize<List<ContributorDto>>(restResponse.Content, _deserializerOptions);

                        result.AddRange(restResult);
                        if (restResult.Count < 50)
                        {
                            break;
                        }
                    }
                    else
                    {
                        await HandleError(restResponse);
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
                            JsonSerializer.Deserialize<List<ContributorDto>>(temp.Content, _deserializerOptions);

                        if (restResult.Count < amount)
                        {
                            result.AddRange(restResult);
                            break;
                        }

                        result.AddRange(restResult.GetRange(0, amount));
                    }
                    else
                    {
                        await HandleError(temp);
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
    private async Task HandleError(RestResponse restResponse)
    {
        var header = restResponse.Headers.FirstOrDefault(x => x.Name == "X-RateLimit-Remaining");
        if (header.Value != null && Convert.ToInt32(header.Value) == 0)
        {
            header = restResponse.Headers.FirstOrDefault(x => x.Name == "X-RateLimit-Reset");
            
            DateTimeOffset utcTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(header.Value));
            DateTime retryTime = utcTime.DateTime;
            _logger.LogWarning("Rate limit reached. Retrying in {seconds} seconds", (int)(retryTime - DateTime.UtcNow).TotalSeconds);
            await Task.Delay(TimeSpan.FromSeconds((int)(retryTime - DateTime.UtcNow).TotalSeconds + 10));
            return;
        }

        header = restResponse.Headers.FirstOrDefault(x => x.Name == "Retry-After");
        if (header is not null)
        {
            _logger.LogWarning("Rate limit reached. Retrying in {seconds} seconds", header.Value);
            await Task.Delay(TimeSpan.FromSeconds(int.Parse(header.Value.ToString() + 1)));
            return;
        }

        restResponse.ThrowIfError();
    }
}