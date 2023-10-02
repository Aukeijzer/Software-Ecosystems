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
    public async Task<List<Project>> GetProjectsByKeywordAsync(string keyword)
    {
        var request = new RestRequest("topic/" + keyword);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<Project>>(request) ?? throw new HttpRequestException();
    }
    public async Task<List<Project>> GetProjectsByTopicAsync(string topic)
    {
        var request = new RestRequest("name/" + topic);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<Project>>(request) ?? throw new HttpRequestException();
    }
}