using RestSharp;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.Spider;

/// <summary>
/// This service is responsible for requesting the Spider for projects.
/// </summary>
public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService(string connectionString)
    {
        var options = new RestClientOptions(connectionString);
        options.MaxTimeout = 100000000;
        _spiderClient = new RestClient(options);
    }
    
    /// <summary>
    /// Requests the Spider for projects related to the given keyword.
    /// </summary>
    public async Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount)
    {
        var request = new RestRequest("name/" + keyword + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }
    
    /// <summary>
    /// Requests the Spider for projects related to the given topic.
    /// </summary>
    public async Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount)
    {
        var request = new RestRequest("topic/" + topic + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    /// <summary>
    /// Requests the Spider to update the given projects. 
    /// </summary>
    public async Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> projectDtos)
    {
        var request = new RestRequest("", Method.Post).AddJsonBody(projectDtos);
        return await _spiderClient.PostAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }

    /// <summary>
    /// Requests the Spider for the contributors of the given project.
    /// </summary>
    public async Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount)
    {
        var request = new RestRequest("contributors/" + projectDto.OwnerName + "/" + projectDto.RepoName + "/" + amount);
        return await _spiderClient.GetAsync<List<ContributorDto>>(request) ?? throw new HttpRequestException();
    }
}