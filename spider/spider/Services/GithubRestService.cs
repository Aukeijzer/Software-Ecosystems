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

    public async Task<List<ContributorDto>> GetRepoContributors(String ownerName, string repoName)
    {
        var request = new RestRequest("repos/" + ownerName + "/" + repoName + "/contributors");
        var result = await _githubRestClient.GetAsync<Object>(request) ?? throw new HttpRequestException();
        return await _githubRestClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
    }
}