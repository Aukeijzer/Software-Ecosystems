using spider.Dtos;
using RestSharp;
using spider.Models.Rest;

namespace spider.Services;

public class GithubRestService : IGithubRestService
{
    private readonly RestClient _GithubRestClient;
    public GithubRestService()
    {
        var options = new RestClientOptions("https://api.github.com");
        _GithubRestClient = new RestClient(options);
    }

    public async Task<Contributors> GetRepoContributors(String ownerName, string repoName)
    {
        var request = new RestRequest("repos/" + ownerName + "/" + repoName + "/contributors");
        return await _GithubRestClient.GetAsync<Contributors>(request) ?? throw new HttpRequestException();
    }
}