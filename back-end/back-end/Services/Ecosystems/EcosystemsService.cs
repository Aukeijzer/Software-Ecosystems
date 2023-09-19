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
    public List<Ecosystem> GetAll()
    {
        return _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .ToList();
    }

    public void Add(Ecosystem ecosystem)
    {
        _dbContext.Ecosystems.Add(ecosystem);
        _dbContext.SaveChanges();
    }

    public Ecosystem? GetById(long id)
    {
        return _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .SingleOrDefault(e => e.Id == id);
    }
    public Ecosystem? GetByName(string name)
    {
        return _dbContext.Ecosystems
            .Include(e => e.Projects)
            .AsNoTracking()
            .SingleOrDefault(e => e.Name == name);
    }
}