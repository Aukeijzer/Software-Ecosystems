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
    /// Returns a list of projects that were created in the given DateRange and contain the given topic.
    /// </summary>
    /// <param name="st"></param>
    /// <param name="et"></param>
    /// <param name="topic"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<List<ProjectDto>> GetProjectsByDate(DateTime st, DateTime et, List<string> topic)
    {
        // Create a query that searches for projects in the given DateRange. 
        string endTime = et.ToString("yyyy-MM-dd'T'HH:mm:ss.ff"),
            startTime = st.ToString("yyyy-MM-dd'T'HH:mm:ss.ff");
        var response = await client.SearchAsync<ProjectDto>(s => s
            .Index("projects-timed-test-02")
            .Query(q => q
                .Range(r => r
                    .DateRange( dr => dr
                        .Field(f => f.Timestamp)
                        .Lte(endTime)
                        .Gte(startTime)
                        .TimeZone("+00:00")
                        .Format("yyyy-MM-dd'T'HH:mm:ss.SS")
                    )
                )
            )
        );
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
        
        return response.Documents.Where(p => topic.All(t => p.Topics.Contains(t))).ToList();
       // return response.Documents.ToList().FindAll(p => p.Topics.Intersect(topic).Any());
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