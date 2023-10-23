using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SECODashBackend.Dtos;

namespace SECODashBackend.Services.ElasticSearch;

public class ElasticsearchService : IElasticsearchService
{
    private const string ProjectIndex = "projects";
    private readonly ElasticsearchClient _client;
    private const int NumberOfRequestedProjects = 1000;
    
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

    public async Task<List<ProjectDto>> GetProjectsByTopic(params string[] topics)
    {
        var response = await _client.SearchAsync<ProjectDto>(s => s 
            .Index(ProjectIndex) 
            .From(0)
            .Size(NumberOfRequestedProjects)
            .Query(q => q
                .TermsSet(t => t
                    .Field(p => p.Topics)
                    .Terms(topics)
                    .MinimumShouldMatchScript( new Script(new InlineScript("params.num_terms"))))));
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
        return response.Documents.ToList();
    }
}