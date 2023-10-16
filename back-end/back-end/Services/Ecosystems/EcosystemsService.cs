using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Models;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.ProgrammingLanguages;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly ISpiderService _spiderService;
    private readonly IDataProcessorService _dataProcessorService;

    public EcosystemsService(EcosystemsContext dbContext, ISpiderService spiderService, IDataProcessorService dataProcessorService)
    {
        _dbContext = dbContext;
        _spiderService = spiderService;
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
        
        // Request the Spider for projects related to this ecosystem.
        var dtos = await _spiderService.GetProjectsByTopicAsync(ecosystem.Name);

        // Check which projects are not already in the Projects list of the ecosystem
        var newProjects = dtos
            .Where(x => !ecosystem.Projects.Exists(y => y.Id == x.Id))
            .Select(ProjectConverter.ToProject);

        // Only add these projects to the database
        ecosystem.Projects.AddRange(newProjects);
        
        // Make the changes persistent by saving them to the database
        await _dbContext.SaveChangesAsync();

        // Get the top languages associated with the ecosystem
        var topLanguages = TopProgrammingLanguagesService.GetTopLanguagesForEcosystem(ecosystem);
        // Add the top languages to the ecosystem
        ecosystem.TopLanguages = topLanguages;
        
        return ecosystem;
    }
}