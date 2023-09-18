using SECODashBackend.Models;

namespace SECODashBackend.Services;

public interface IProjectsService
{
    public List<Project> GetAll();
}