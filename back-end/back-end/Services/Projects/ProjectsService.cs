using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;
using SECODashBackend.Services.ElasticSearch;

namespace SECODashBackend.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly IElasticsearchService _elasticsearchService;

    public ProjectsService(EcosystemsContext dbContext, IElasticsearchService elasticsearchService)
    {
        _dbContext = dbContext;
        _elasticsearchService = elasticsearchService;
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
}