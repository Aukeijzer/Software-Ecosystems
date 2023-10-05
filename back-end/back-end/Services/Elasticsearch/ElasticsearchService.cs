using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dto;

namespace SECODashBackend.Services.ElasticSearch;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticsearchClient _client;
    public ElasticsearchService(ElasticsearchClient client)
    {
        _client = client;
    }


    public async Task AddProjects(ProjectDto projectDto)
    {
        var response = await _client.IndexAsync(projectDto, "projects");
    }
}