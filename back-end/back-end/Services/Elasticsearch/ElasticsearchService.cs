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
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }

    public async Task<List<ProjectDto>> GetProjectsByTopic(string topic, int amount)
    {
        var response = await _client.SearchAsync<ProjectDto>(s => s 
            .Index(ProjectIndex) 
            .From(0)
            .Size(amount)
            .Query(q => q
                .Term(t => t.Topics, topic) 
            )
        );
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
        return response.Documents.ToList();
    }
}