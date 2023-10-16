using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SECODashBackend.Dto;

namespace SECODashBackend.Services.ElasticSearch;

public class ElasticsearchService : IElasticsearchService
{
    private const string ProjectIndex = "projects";
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
    }
}