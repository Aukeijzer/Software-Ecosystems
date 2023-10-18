using spider.Dtos;
using RestSharp;
using RestSharp.Authenticators;
using spider.Models.Rest;

namespace spider.Services;

public class GithubRestService : IGithubRestService
{
    private readonly RestClient _githubRestClient;
    public GithubRestService()
    {
        var options = new RestClientOptions("https://api.github.com");
        _githubRestClient = new RestClient(options);
    }

    public async Task<Contributors> GetRepoContributors(String ownerName, string repoName)
    {
        var request = new RestRequest("repos/" + ownerName + "/" + repoName + "/contributors");
        return await _githubRestClient.GetAsync<Contributors>(request) ?? throw new HttpRequestException();
    }
}