using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Models;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.ProgrammingLanguages;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly IDataProcessorService _dataProcessorService;
    private readonly IProjectsService _projectsService;

    public EcosystemsService(
        EcosystemsContext dbContext,
        IProjectsService projectsService,
        IDataProcessorService dataProcessorService)
    {
        _dbContext = dbContext;
        _projectsService = projectsService;
        _dataProcessorService = dataProcessorService;
    }
    public async Task<List<Ecosystem>?> GetAllAsync()
    {
        return await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .ThenInclude(p => p.Languages)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> AddAsync(Ecosystem ecosystem)
    {
        await _dbContext.Ecosystems.AddAsync(ecosystem);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Ecosystem?> GetByIdAsync(string id)
    {
        return await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .ThenInclude(p => p.Languages)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Ecosystem?> GetByNameAsync(string name)
    {
        var ecosystem = await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .ThenInclude(p => p.Languages)
            .SingleOrDefaultAsync(e => e.Name == name);
        if (ecosystem == null) return null;
        
        
        if (ecosystem == null) throw new KeyNotFoundException();

        var ecosystemDto = EcosystemConverter.ToDto(ecosystem);
        
        // Retrieve the projects belonging to the ecosystem
        var projects = await _projectsService.GetByTopicAsync(ecosystem.Name);
        var projectList = projects.ToList();

        // Get the most popular programming languages associated with the ecosystem
        var topLanguages = TopProgrammingLanguagesService.GetTopLanguagesForEcosystem(projectList);
        // Add the top languages to the ecosystem
        ecosystem.TopLanguages = topLanguages;
        
        // Add the projects and languages to the ecosystem dto
        ecosystemDto.Projects = projectList.Select(ProjectConverter.ToProjectDto);
        ecosystemDto.TopLanguages = topLanguages;
        
        return ecosystem;
    }
}