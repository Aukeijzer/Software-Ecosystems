using SECODashBackend.Dtos;

namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   public Task<List<ProjectDto>> GetProjectsByTopic(string topic, int amount = 1000);
}