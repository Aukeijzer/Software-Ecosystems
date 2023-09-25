using RestSharp;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Spider;

public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService()
    {
        var options = new RestClientOptions("http://localhost:5205/Spider");
        _spiderClient = new RestClient(options);
    }
    public async Task<List<Project>?> GetProjectsByNameAsync(string name)
    {
        var request = new RestRequest(name);
        return await _spiderClient.GetAsync<List<Project>>(request);
    }
}