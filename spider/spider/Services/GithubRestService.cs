using spider.Dtos;
using RestSharp;

namespace spider.Services;

public class GithubRestService : IGithubRestService
{
    private readonly RestClient _githubRestClient;
    public GithubRestService()
    {
        var options = new RestClientOptions("https://api.github.com");
        _githubRestClient = new RestClient(options);
        _githubRestClient.AddDefaultHeader("Authorization", "Bearer " + Environment.GetEnvironmentVariable("API_Token"));
    }

    public async Task<List<ContributorDto>> GetRepoContributors(String ownerName, string repoName, int amount = 50)
    {
        var result = new List<ContributorDto>();
        var request = new RestRequest("repos/" + repoName + "/" + ownerName + "/contributors");
        request.AddParameter("per_page", 50);
        int page = 1;
        while (amount > 0)
        {
            if (amount > 50)
            {
                request.AddOrUpdateParameter("page", page);
                var temp =  await _githubRestClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
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
                request.AddOrUpdateParameter("page", page);
                var temp =  await _githubRestClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
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