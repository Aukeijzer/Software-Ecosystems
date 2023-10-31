using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Services;

public interface IGitHubGraphqlService
{
    public Task<SpiderData> QueryRepositoriesByName(string name, int amount = 10);

    public Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10);
    
    public Task<RepositoryWrapper> QueryRepositoryByName(string repoName, string ownerName);

    public Task<SpiderData> ToQueryString(List<ProjectRequestDto> repos);

}