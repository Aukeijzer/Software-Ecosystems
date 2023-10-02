using spider.Models;

namespace spider.Services;

public interface IGitHubService
{
    public Task<SpiderData> QueryRepositoriesByName(string name, int amount = 10, string readmeName = "main:README.md");

    public Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10, string readmeName = "main:README.md");

}