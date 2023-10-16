using spider.Models;
using spider.Dtos;
using RestSharp;

namespace spider.Services;

public class GithubRestService : IGithubRestService
{
    private readonly RestClient _GithubRestClient;
    public GithubRestService()
    {
        var options = new RestClientOptions("https://api.github.com");
        _GithubRestClient = new RestClient(options);
    }

    public async Task<> GetRepoContributors(String ownerName, string repoName)
    {
        var request = new RestRequest("repos/" + ownerName + "/" + repoName + "/contributors");
        return await _GithubRestClient.GetAsync<>(request) ?? throw new HttpRequestException();
    }
}