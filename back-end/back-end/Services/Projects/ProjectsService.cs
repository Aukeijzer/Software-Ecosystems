using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ISpiderService _spiderService;

    public ProjectsService(
        IElasticsearchService elasticsearchService,
        ISpiderService spiderService)
    {
        _elasticsearchService = elasticsearchService;
        _spiderService = spiderService;
    }

    public async Task MineByTopicAsync(string topic, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await _spiderService.GetProjectsByTopicAsync(topic, amount);
        
        // Save these projects to elasticsearch
        await _elasticsearchService.AddProjects(newDtos);
    }
    
    public async Task MineByKeywordAsync(string keyword, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await _spiderService.GetProjectsByKeywordAsync(keyword, amount);
        
        // Save these projects to elasticsearch
        await _elasticsearchService.AddProjects(newDtos);
    }
}