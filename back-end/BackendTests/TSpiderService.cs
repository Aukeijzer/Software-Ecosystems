using SECODashBackend.Dtos.Project;
using spider.Dtos;
namespace BackendTests;

public class TSpiderService
{
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount)
    {
        // Return an empty list
        return new Task<List<ProjectDto>>(() => new List<ProjectDto>());
    }

    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount)
    {
        return new Task<List<ProjectDto>>(() => new List<ProjectDto>());
    }

    public Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> dtos)
    {
        return new Task<List<ProjectDto>>(() => new List<ProjectDto>());
    }

    public Task<List<ContributorDto>> GetContributors(ProjectRequestDto projectDto, int amount)
    {
        return new Task<List<ContributorDto>>(() => new List<ContributorDto>());
    }
}