using SECODashBackend.Dtos;

namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   public Task<List<ProjectDto>> GetProjectsByTopic(params string[] topics);
}