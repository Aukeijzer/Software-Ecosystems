using RestSharp;
using SECODashBackend.Dtos.Project;
using spider.Dtos;

namespace SECODashBackend.Services.Spider;

public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService()
    {
        var options = new RestClientOptions("http://localhost:5205/Spider");
        _spiderClient = new RestClient(options);
    }
    public async Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword)
    {
        var request = new RestRequest("name/" + keyword);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }
    public async Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic)
    {
        var request = new RestRequest("topic/" + topic);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    public async Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> projectDtos)
    {
        var request = new RestRequest("", Method.Post).AddJsonBody(projectDtos);
        return await _spiderClient.PostAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    public async Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount = 50)
    {
        var request = new RestRequest("contributors/" + projectDto.OwnerName + "/" + projectDto.RepoName + "/" + amount);
        return await _spiderClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
    }
}