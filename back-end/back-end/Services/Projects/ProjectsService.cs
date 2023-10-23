using SECODashBackend.DataConverters;
using SECODashBackend.Models;
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

    public async Task<Project?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Project>> GetByTopicAsync(params string[] topics)
    {
        // Request the Spider for new projects related to this ecosystem.
        var newDtos = await _spiderService.GetProjectsByTopicAsync(topics.First());
        
        // Save these projects to elasticsearch
        await _elasticsearchService.AddProjects(newDtos);
        
        // Retrieve all related projects from elasticsearch
        var dtos = await _elasticsearchService.GetProjectsByTopic(topics);
        return dtos.Select(ProjectConverter.ToProject);
    }
}