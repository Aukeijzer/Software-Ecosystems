using RestSharp;
using SECODashBackend.Dtos.Project;
using spider.Dtos;

namespace SECODashBackend.Services.Spider;

public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService(string connectionString)
    {
        var options = new RestClientOptions(connectionString);
        options.MaxTimeout = 100000000;
        _spiderClient = new RestClient(options);
    }
    public async Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount)
    {
        var request = new RestRequest("name/" + keyword + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }
    public async Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount)
    {
        var request = new RestRequest("topic/" + topic + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    public async Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> projectDtos)
    {
        var request = new RestRequest("", Method.Post).AddJsonBody(projectDtos);
        return await _spiderClient.PostAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    public async Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount)
    {
        var request = new RestRequest("contributors/" + projectDto.OwnerName + "/" + projectDto.RepoName + "/" + amount);
        return await _spiderClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
    }
}