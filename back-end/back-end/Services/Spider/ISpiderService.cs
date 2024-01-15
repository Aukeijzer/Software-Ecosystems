using SECODashBackend.Dtos;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.Spider;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects.
/// </summary>
public interface ISpiderService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic.
    /// </summary>
    /// <param name="topic">The topic to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount);
    /// <summary>
    /// Requests the Spider for projects related to the given keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount);
    public Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> dtos);
    public Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount);
}