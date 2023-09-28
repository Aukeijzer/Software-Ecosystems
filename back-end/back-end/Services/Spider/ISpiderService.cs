using SECODashBackend.Models;

namespace SECODashBackend.Services.Spider;

public interface ISpiderService
{
    public Task<List<Project>> GetProjectsByNameAsync(string name);
}