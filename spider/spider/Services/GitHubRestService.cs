using spider.Dtos;
using RestSharp;

namespace spider.Services;

public class GitHubRestService : IGitHubRestService
{
    private readonly RestClient _gitHubRestClient;
    private readonly ILogger<GitHubGraphqlService> _logger;
    public GitHubRestService(ILogger<GitHubGraphqlService> logger)
    {
        var options = new RestClientOptions("https://api.github.com");
        _gitHubRestClient = new RestClient(options);
        _gitHubRestClient.AddDefaultHeader("Authorization", "Bearer " + Environment.GetEnvironmentVariable(
            "API_Token"));
        _gitHubRestClient.AddDefaultHeader("X-Github-Next-Global-ID", "1");
        _logger = logger;
    }

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
                    var temp = await _gitHubRestClient.GetAsync<List<ContributorDto>>(request);
                    if (temp == null)
                    {
                        return result;
                    }
                    result.AddRange(temp);
                    if (temp.Count < 50)
                    {
                        break;
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
                    var temp = await _gitHubRestClient.GetAsync<List<ContributorDto>>(request);
                    if (temp == null)
                    {
                        return result;
                    }
                    if (temp.Count < amount)
                    {
                        result.AddRange(temp);
                        break;
                    }

                    result.AddRange(temp.GetRange(0, amount));
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
}