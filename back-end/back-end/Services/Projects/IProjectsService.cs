using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Projects;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects and saving them to Elasticsearch.
/// </summary>
public interface IProjectsService
{
    public Task MineByTopicAsync(string topic, int amount);
    public Task MineByKeywordAsync(string topic, int amount);
    public Task<int> GetByTimeFrameAsync(DateTime st, DateTime et, string topic);
}