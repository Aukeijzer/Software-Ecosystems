using SECODashBackend.Dto;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Spider;

public interface ISpiderService
{
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic);
    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword);
}