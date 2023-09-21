using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private readonly EcosystemsContext _dbContext;

    public EcosystemsService(EcosystemsContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Ecosystem>> GetAllAsync()
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
        return await _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Name == name);
    }
}