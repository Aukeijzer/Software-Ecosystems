using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SECODashBackend.Dtos.Ecosystem;
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
    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest();
        var indexOperations = 
            projectDtos.Select(p =>
            {
                p.Timestamp = DateTime.UtcNow;
                return new BulkIndexOperation<ProjectDto>(p);
                
            projectDtos.Select(p => new BulkIndexOperation<ProjectDto>(p)
            {
                Index = "testtest"
            });
        request.Operations = new BulkOperationsCollection(indexOperations);
        var response = await client.BulkAsync(request);
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }
    
    /// <summary>
    /// Retrieve all related projects from elasticsearch falling in the given time frame.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<List<ProjectDto>> GetProjectsByDate(DateTime time)
    {
        // Make the time frame a bit bigger to account for the time it takes to mine the projects.
        time = time.AddDays(1);
        SearchResponse<ProjectDto> response = await client.SearchAsync<ProjectDto>(s => s
            .Query(q => q.Term(r => r.Timestamp, time)));
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
        
        return response.Documents.ToList();
    }

    /// <summary>
    /// Queries the Elasticsearch index for projects that match the given search request. 
    /// </summary>
    public async Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest)
    {
        var response = await client
            .SearchAsync<ProjectDto>(searchRequest);
        return response.IsValidResponse ? response : throw new HttpRequestException(response.ToString());
    }
}