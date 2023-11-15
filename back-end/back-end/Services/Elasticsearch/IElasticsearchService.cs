using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   public Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest);
}