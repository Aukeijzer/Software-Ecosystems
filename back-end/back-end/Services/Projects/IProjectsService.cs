using Elastic.Clients.Elasticsearch;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Projects;

public interface IProjectsService
{
    public Task<Project?> GetByIdAsync(string id);
    public Task<IEnumerable<Project>> GetByTopicsAsync(List<string> topics);
    public Task MineByTopicAsync(string topic);
    Task<IEnumerable<Project>> GetByTimeFrameAsync(DateTime time);
}