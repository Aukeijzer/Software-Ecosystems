using SECODashBackend.Models;

namespace SECODashBackend.Services.Projects;

public interface IProjectsService
{
    public Task<List<Project>> GetAllAsync();
    public Task<int> AddAsync(Project project);
    public Task<Project?> GetByIdAsync(string id);
    public Task<IEnumerable<Project>> GetByTopicAsync(params string[] topics);
}