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
   
    /// <summary>
    /// Adds the given projects to the Elasticsearch index. 
    /// </summary>
    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest();
        var indexOperations = 
            projectDtos.Select(p =>
            {
                return new BulkIndexOperation<ProjectDto>(p);
            });
        
        request.Operations = new BulkOperationsCollection(indexOperations);
        var response = await client.BulkAsync(request);
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }
    
    /// <summary>
    /// Returns a project count of the projects that were created in the given DateRange and contain the given topic
    /// </summary>
    public async Task<long> GetProjectsByDate(DateTime rawStartTime, DateTime rawEndTime, string topic)
    {
        string endTime = rawEndTime.ToString("yyyy-MM-dd'T'HH:mm:ss.ff"),
            startTime = rawStartTime.ToString("yyyy-MM-dd'T'HH:mm:ss.ff");
        
        // Create a query that searches for project count in the given DateRange with the give topic. 
        var response = await client.CountAsync<ProjectDto>(s => s
            .Query(q => q
                .Bool(b => b.
                    Must(mu => mu
                        .Match(m => m
                            .Field(f => f.Topics)
                            .Query(topic)
                        ),
                        mu => mu
                            .Range(r => r
                                .DateRange(dr => dr
                                    .Field(f => f.LatestDefaultBranchCommitDate)
                                    .Gte(startTime)
                                    .Lte(endTime)
                                )
                            )
                    )
                )
            )
        );
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString()); 
        
        return response.Count;
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