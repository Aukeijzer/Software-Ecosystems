using spider.Dtos;
using spider.Models.Graphql;

namespace spider.Services;

public interface IGitHubGraphqlService
{
    
    /// <summary>
    /// QueryRepositoriesByNameHelper splits the incoming request into smaller parts
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from</param>
    /// <returns>list of repositories in the form of List&lt;SpiderData&gt;</returns>
    public Task<List<SpiderData>> QueryRepositoriesByNameHelper(string name, int amount = 10,
        string? startCursor = null);
    
    /// <summary>
    /// QueryRepositoriesByName sends a graphql request to the github api and returns on success and otherwise handles
    /// the error and retries if necessary.
    /// </summary>
    /// <param name="repositoryName">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="cursor">The cursor to start the search from</param>
    /// <param name="tries">amount of retries before failing</param>
    /// <returns>list of repositories in the form of SpiderData</returns>
    /// <exception cref="BadHttpRequestException">If it fails after tries amount of retries throw</exception>
    public Task<SpiderData> QueryRepositoriesByName(string name, int amount = 10, string? cursor = null, int tries = 3);

    /// <summary>
    /// QueryRepositoriesByTopicHelper splits the incoming request into smaller parts
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from</param>
    /// <returns>list of repositories in the form of List&lt;TopicSearchData&gt;</returns>
    public Task<List<TopicSearchData>> QueryRepositoriesByTopicHelper(String topic, int amount,
        string? startCursor = null);
    
    /// <summary>
    /// QueryRepositoriesByTopic sends a graphql request to the github api and returns on success and otherwise handles
    /// the error and retries if necessary.
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="cursor">The cursor to start the search from</param>
    /// <param name="tries">amount of retries before failing</param>
    /// <returns>list of repositories in the form of TopicSearchData</returns>
    /// <exception cref="BadHttpRequestException">If it fails after tries amount of retries throw</exception>
    public Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10, string? cursor = null, int tries = 3);
    
    /// <summary>
    /// QueryRepositoryByName sends a graphql request to the github api and returns on success. Does not handle errors
    /// yet
    /// </summary>
    /// <param name="repositoryName">Name of the repository</param>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <returns>repository in the form of RepositoryWrapper</returns>
    public Task<RepositoryWrapper> QueryRepositoryByName(string repoName, string ownerName);

    /// <summary>
    /// ToQueryString converts ProjectRequestDtos into a format that can be inserted into a graphql search query and
    /// sends the query using QueryRepositoriesByName
    /// </summary>
    /// <param name="repos">A list of repository names and owner names</param>
    /// <returns>list of repositories in the form of SpiderData</returns>
    public Task<List<SpiderData>> GetByNames(List<ProjectRequestDto> repos);

}