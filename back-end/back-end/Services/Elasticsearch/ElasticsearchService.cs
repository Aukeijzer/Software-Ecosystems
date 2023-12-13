using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.Project;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace SECODashBackend.Services.ElasticSearch;

/// <summary>
///  Service that is responsible for handling all Elasticsearch related requests.
/// </summary>
public class ElasticsearchService(ElasticsearchClient client) : IElasticsearchService
{
    private const string ProjectIndex = "projects-timed-test-02";
   
    /// <summary>
    /// Adds the given projects to the Elasticsearch index. 
    /// </summary>
    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest(ProjectIndex);
        var indexOperations = 
            projectDtos.Select(p =>
            {
                p.Timestamp = p.LatestDefaultBranchCommitDate;
                return new BulkIndexOperation<ProjectDto>(p);
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
        // Make the time frame bigger to account for the time it takes to mine the projects.
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