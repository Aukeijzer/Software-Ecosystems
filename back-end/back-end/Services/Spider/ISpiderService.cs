using SECODashBackend.Dtos;

namespace SECODashBackend.Services.Spider;

public interface ISpiderService
{
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic);
    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword);
    public Task<List<ProjectDto>> UpdateProjects(List<ProjectRequestDto> dtos);
}