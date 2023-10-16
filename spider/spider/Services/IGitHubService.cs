using spider.Dtos;
using spider.Models;

namespace spider.Services;

public interface IGitHubService
{
    public Task<SpiderData> QueryRepositoriesByName(string name, int amount = 10);

    public Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10);
    
    public Task<RepositoryWrapper> QueryRepositoryByName(string repoName, string ownerName);

    public Task<SpiderData> ToQueryString(List<ProjectRequestDto> repos);

}