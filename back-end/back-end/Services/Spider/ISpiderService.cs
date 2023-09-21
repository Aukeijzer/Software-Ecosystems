using SECODashBackend.Models;

namespace SECODashBackend.Spider;

public interface ISpiderService
{
    public Task<List<Project>?> GetProjectsByNameAsync(string name);
}