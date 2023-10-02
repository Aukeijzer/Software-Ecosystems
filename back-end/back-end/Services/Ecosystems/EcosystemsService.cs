using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly ISpiderService _spiderService;

    public EcosystemsService(EcosystemsContext dbContext, ISpiderService spiderService)
    {
        _dbContext = dbContext;
        _spiderService = spiderService;
    }
    public async Task<List<Ecosystem>?> GetAllAsync()
    {
        return await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> AddAsync(Ecosystem ecosystem)
    {
        await _dbContext.Ecosystems.AddAsync(ecosystem);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Ecosystem?> GetByIdAsync(long id)
    {
        return await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
    }
    public async Task<Ecosystem?> GetByNameAsync(string name)
    {
        var ecosystem = await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .SingleOrDefaultAsync(e => e.Name == name);
        if (ecosystem == null) return null;
        
        // Request the Spider for projects related to this ecosystem.
        var newProjects = await _spiderService.GetProjectsByTopicAsync(ecosystem.Name);
        
        // Only add the projects if they are not already in the database.
        ecosystem.Projects.AddRange(
            newProjects.Where(x => !ecosystem.Projects.Exists(y => y.Id == x.Id)));
        await _dbContext.SaveChangesAsync();
        return ecosystem;
    }
}