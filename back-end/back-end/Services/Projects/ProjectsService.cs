using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos;
using SECODashBackend.Models;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ISpiderService _spiderService;

    public ProjectsService(
        EcosystemsContext dbContext,
        IElasticsearchService elasticsearchService,
        ISpiderService spiderService)
    {
        _dbContext = dbContext;
        _elasticsearchService = elasticsearchService;
        _spiderService = spiderService;
    }
    public async Task<List<Project>> GetAllAsync()
    {
        return await _dbContext.Projects
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> AddAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Project?> GetByIdAsync(string id)
    {
        return await _dbContext.Projects
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);
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