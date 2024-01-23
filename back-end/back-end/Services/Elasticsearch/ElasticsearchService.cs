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
    
    /// <summary>
    /// Queries the Elasticsearch index for the number of projects that were created before the given start time,
    /// updated since the start time and contain all the given topics.
    /// </summary>
    /// <param name="rawStartTime">The start of the date range</param>
    /// <param name="topics">The topics to search for</param>
    /// <returns>The number of projects that were created before the given start time,
    /// updated since the start time and contain the given topics</returns>
    public async Task<long> GetProjectCountByDate(DateTime rawStartTime, List<string> topics)
    {
        var startTime = rawStartTime.ToString("yyyy-MM-dd'T'HH:mm:ss.ff");
        
        // Create a query that searches for project count in the given DateRange with the give topic. 
        var response = await client.CountAsync<ProjectDto>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                        .TermsSet(c => c
                            .Field("topics.keyword")
                            .Terms(topics)
                            .MinimumShouldMatchScript(new Script(new InlineScript("params.num_terms")))
                        ),
                        mu => mu
                            .Range(r => r.
                                DateRange(dr => dr
                                    .Field(f => f.CreatedAt)
                                    .Lte(startTime)
                            )
                        ),
                        mu => mu
                            .Range(r => r.
                                DateRange(dr => dr
                                    .Field(f => f.LatestDefaultBranchCommitDate)
                                    .Gte(startTime)
                            )
                        )
                    )
                )
            )
        );
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString()); 
        
        return response.Count;
    }
}