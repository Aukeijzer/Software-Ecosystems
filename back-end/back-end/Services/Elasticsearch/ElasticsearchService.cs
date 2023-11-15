using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.ElasticSearch;

public class ElasticsearchService : IElasticsearchService
{
    // Name of the projects index in Elasticsearch
    private const string ProjectIndex = "projects-01";

    private readonly ElasticsearchClient _client;
    public ElasticsearchService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest(ProjectIndex);
        var indexOperations = 
            projectDtos.Select(p => new BulkIndexOperation<ProjectDto>(p));
        request.Operations = new BulkOperationsCollection(indexOperations);
        var response = await _client.BulkAsync(request);
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }
    
    public async Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest)
    {
        var response = await _client
            .SearchAsync<ProjectDto>(searchRequest);
        return response.IsValidResponse ? response : throw new HttpRequestException(response.ToString());
    }
}