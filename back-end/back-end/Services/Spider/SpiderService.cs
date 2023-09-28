using RestSharp;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Spider;

public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService()
    {
        var options = new RestClientOptions("http://localhost:5205");
        _spiderClient = new RestClient(options);
    }
    public async Task<List<Project>> GetProjectsByNameAsync(string name)
    {
        var request = new RestRequest($"Spider/{name}");
        
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<Project>>(request) ?? throw new HttpRequestException();
    }
}