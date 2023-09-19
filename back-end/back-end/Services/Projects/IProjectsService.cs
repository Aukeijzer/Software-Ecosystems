using SECODashBackend.Models;

namespace SECODashBackend.Services.Projects;

public interface IProjectsService
{
    public List<Project> GetAll();
    public void Add(Project project);
    public Project? GetById(long id);
}