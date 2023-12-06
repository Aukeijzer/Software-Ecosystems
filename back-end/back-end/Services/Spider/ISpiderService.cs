using SECODashBackend.Dtos;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.Spider;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects.
/// </summary>
public interface ISpiderService
{
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount);
    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount);
    public Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> dtos);
    public Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount);
}