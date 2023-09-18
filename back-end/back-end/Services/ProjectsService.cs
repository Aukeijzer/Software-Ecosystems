using SECODashBackend.Models;

namespace SECODashBackend.Services;

public class ProjectsService : IProjectsService
{
    public List<Project> GetAll()
    {
        return new List<Project>{new("awesome-agriculture", 1, "Open source technology for agriculture, farming, and gardening", null, null, 1100)};
    }
}