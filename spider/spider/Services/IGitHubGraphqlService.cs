using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Services;

public interface IGitHubGraphqlService
{
    public Task<List<SpiderData>> QueryRepositoriesByNameHelper(string name, int amount = 10,
        string? startCursor = null);
    public Task<SpiderData> QueryRepositoriesByName(string name, int amount = 10, string? cursor = null, int tries = 3);

    public Task<List<TopicSearchData>> QueryRepositoriesByTopicHelper(String topic, int amount,
        string? startCursor = null);
    public Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10, string? cursor = null, int tries = 3);
    
    public Task<RepositoryWrapper> QueryRepositoryByName(string repoName, string ownerName);

    public Task<SpiderData> ToQueryString(List<ProjectRequestDto> repos);

}