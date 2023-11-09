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

    public async Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto)
    {
        var request = new RestRequest("contributors/" + projectDto.OwnerName + "/" + projectDto.RepoName);
        return await _spiderClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
    }
}