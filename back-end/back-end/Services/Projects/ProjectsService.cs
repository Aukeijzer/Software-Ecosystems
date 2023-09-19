using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly EcosystemsContext _dbContext;

    public ProjectsService(EcosystemsContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<Project> GetAll()
    {
        return _dbContext.Projects.ToList();
    }

    public void Add(Project project)
    {
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
    }

    public Project? GetById(long id)
    {
        return _dbContext.Projects
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }
}