using SECODashBackend.Dto;

namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
}