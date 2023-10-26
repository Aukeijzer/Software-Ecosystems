using spider.Dtos;
using RestSharp;

namespace spider.Services;

public class GitHubRestService : IGitHubRestService
{
    private readonly RestClient _gitHubRestClient;
    public GitHubRestService()
    {
        var options = new RestClientOptions("https://api.github.com");
        _gitHubRestClient = new RestClient(options);
        _gitHubRestClient.AddDefaultHeader("Authorization", "Bearer " + Environment.GetEnvironmentVariable("API_Token"));
        _gitHubRestClient.AddDefaultHeader("X-Github-Next-Global-ID", "1");
    }

    public async Task<List<ContributorDto>> GetRepoContributors(String ownerName, string repoName, int amount = 50)
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
                var temp =  await _gitHubRestClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
                result = result.Concat(temp).ToList();
                if (temp.Count < 50)
                {
                    break;
                }
                page++;
                amount -= 50;
            }
            else
            {
                request.AddQueryParameter("page", page);
                var temp =  await _gitHubRestClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
                if (temp.Count < amount)
                {
                    result = result.Concat(temp).ToList();
                    break;
                }
                result = result.Concat(temp.GetRange(0,amount)).ToList();
                amount = 0;
            }
        }

        return result;
    }
}