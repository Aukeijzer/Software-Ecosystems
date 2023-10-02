using SECODashBackend.Models;

namespace SECODashBackend.Services.Spider;

public interface ISpiderService
{
    public Task<List<Project>> GetProjectsByTopicAsync(string topic);
    public Task<List<Project>> GetProjectsByKeywordAsync(string keyword);
}