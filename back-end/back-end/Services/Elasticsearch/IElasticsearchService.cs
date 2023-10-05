using SECODashBackend.Dto;

namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(ProjectDto projectDto);
}