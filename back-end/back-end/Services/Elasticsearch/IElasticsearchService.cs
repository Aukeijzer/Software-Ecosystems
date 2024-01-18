using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.ElasticSearch;

/// <summary>
/// Interface for the service that is responsible for handling all Elasticsearch related requests. 
/// </summary>
public interface IElasticsearchService
{
   /// <summary>
   /// Adds the given projects to the Elasticsearch index. 
   /// </summary>
   /// <param name="projectDtos">The projects to be added to the index.</param>
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   /// <summary>
   /// Queries the Elasticsearch index for projects that match the given search request. 
   /// </summary>
   /// <param name="searchRequest">The search request.</param>
   /// <returns>A SearchResponse for the projects that match the search request.</returns>
   public Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest);
   public Task<long> GetProjectsByDate(DateTime startTime, DateTime endTime, string topic);
}