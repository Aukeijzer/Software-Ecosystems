using System.Text.Json;
using spider.Dtos;
using RestSharp;
using RestSharp.Serializers.Json;

namespace spider.Services;

public class GitHubRestService : IGitHubRestService
{
    private readonly RestClient _gitHubRestClient;
    private readonly ILogger<GitHubGraphqlService> _logger;
    private readonly SystemTextJsonSerializer _jsonSerializer;
    public GitHubRestService(ILogger<GitHubGraphqlService> logger)
    {
        var options = new RestClientOptions("https://api.github.com");
        _gitHubRestClient = new RestClient(options);
        _gitHubRestClient.AddDefaultHeader("Authorization", "Bearer " + Environment.GetEnvironmentVariable(
            "API_Token"));
        _gitHubRestClient.AddDefaultHeader("X-Github-Next-Global-ID", "1");
        _logger = logger;
        _jsonSerializer = new SystemTextJsonSerializer();
    }

    
    //GetRepoContributors sends a rest request to the github api and returns on success and otherwise handles the
    //error and retries if necessary.
    public async Task<List<ContributorDto>?> GetRepoContributors(string ownerName, string repoName, int amount = 50)
    {
        var result = new List<ContributorDto>();
        var request = new RestRequest("repos/" + repoName + "/" + ownerName + "/contributors");
        request.AddQueryParameter("per_page", 50);
        int page = 1;
        while (amount > 0)
        {
            if (amount > 50)
            {
                request.AddQueryParameter("page", page);
                try
                {
                    var temp = await _gitHubRestClient.ExecuteAsync(request).ConfigureAwait(false);
                    if (temp.IsSuccessful)
                    {
                        if (temp.Content == null)
                        {
                            return result;
                        }
                        
                        List<ContributorDto> restResult =
                            _jsonSerializer.Deserializer.Deserialize<List<ContributorDto>>(temp);

                        result.AddRange(restResult);
                        if (restResult.Count < 50)
                        {
                            break;
                        }
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
                        if (temp.Content == null)
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

    
    //HandleErrors checks if there is a rate-limit error and if there is, it retries
    private static void HandleError(RestResponse temp)
    {
        var header = temp.Headers.First(x => x.Name == "X-RateLimit-Remaining");
        if (header.Value == "0")
        {
            header = temp.Headers.First(x => x.Name == "X-RateLimit-Reset");

            DateTime retryTime = DateTime.Parse(header.Value.ToString());
            Thread.Sleep((int)(retryTime - DateTime.Now).TotalMilliseconds);
            return;
        }

        header = temp.Headers.FirstOrDefault(x => x.Name == "Retry-After");
        if (header is not null)
        {
            Thread.Sleep(int.Parse(header.Value.ToString()) * 1000);
            return;
        }

        temp.ThrowIfError();
    }
}