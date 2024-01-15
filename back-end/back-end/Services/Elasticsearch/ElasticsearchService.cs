using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.ElasticSearch;

/// <summary>
///  Service that is responsible for handling all Elasticsearch related requests.
/// </summary>
public class ElasticsearchService(ElasticsearchClient client) : IElasticsearchService
{
    /// <summary>
    /// Adds the given projects to the Elasticsearch index. 
    /// </summary>
    /// <param name="projectDtos">The projects to be added to the index.</param>
    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest();
        var indexOperations = 
            projectDtos.Select(p => new BulkIndexOperation<ProjectDto>(p));
        request.Operations = new BulkOperationsCollection(indexOperations);
        var response = await client.BulkAsync(request);
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }
   
    /// <summary>
    /// Queries the Elasticsearch index for projects that match the given search request. 
    /// </summary>
    /// <param name="searchRequest">The search request.</param>
    /// <returns>A SearchResponse for the projects that match the search request.</returns>
    public async Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest)
    {
        var response = await client
            .SearchAsync<ProjectDto>(searchRequest);
        return response.IsValidResponse ? response : throw new HttpRequestException(response.ToString());
    }
}