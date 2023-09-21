using spider.Models;

namespace spider.Services;

public interface IGitHubService
{
    public Task<SpiderData> QueryRepositoriesByName(string name);

}