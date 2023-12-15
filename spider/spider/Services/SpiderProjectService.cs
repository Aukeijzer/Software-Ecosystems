using System.Net;
using System.Text.Json;
using spider.Converters;
using spider.Dtos;

namespace spider.Services;

public class SpiderProjectService : ISpiderProjectService
{
    private readonly ILogger<SpiderProjectService> _logger;
    private readonly IGitHubGraphqlService _gitHubGraphqlService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;
    private readonly IGitHubRestService _gitHubRestService;
    
    public SpiderProjectService(IGitHubGraphqlService gitHubGraphqlService, IGraphqlDataConverter graphqlDataConverter,
        IGitHubRestService gitHubRestService)
    {
        _logger = new Logger<SpiderProjectService>(new LoggerFactory());
        _gitHubGraphqlService = gitHubGraphqlService;
        _graphqlDataConverter = graphqlDataConverter;
        _gitHubRestService = gitHubRestService;
    }
    
    /// <summary>
    /// GetByKeyword takes a keyword, an amount and a start cursor and uses these to find the first amount of projects
    /// after the start cursor with the keyword as search phrase. The result includes contributors.
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns>A list of repositories including contributors in the form of List&lt;ProjectDto&gt;</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ProjectDto>> GetByKeyword(string name, int amount, string? startCursor)
    {
        try
        {
            var listResult = await _gitHubGraphqlService.QueryRepositoriesByNameHelper(name, amount, startCursor);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var spiderData in listResult)
            {
                var temp = _graphqlDataConverter.SearchToProjects(spiderData);
                var tasks = temp.Select(async project =>
                {
                    var response = await GetContributorsByName(project.Name, project.Owner, 100);
                    project.Contributors = response;
                });
                await Task.WhenAll(tasks);
                result.AddRange(temp);
            }

            _logger.LogInformation("{Origin}: Returning the project with name: {name}.", this, name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " in {origin} with request: \"{name}\"", this, name);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }
    }
    
    /// <summary>
    /// GetByTopic takes the name of a topic, an amount and a start cursor and uses those to get the first amount of
    /// repositories with the topic, after the start cursor. The result includes contributors.
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ProjectDto>> GetByTopic(string topic, int amount, string? startCursor)
    {
        try
        {
            var listResult = await _gitHubGraphqlService.QueryRepositoriesByTopicHelper(topic, amount, startCursor);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var topicSearchData in listResult)
            {
                var temp = _graphqlDataConverter.TopicSearchToProjects(topicSearchData);
                 var tasks = temp.Select(async project =>
                 {
                     var response = await GetContributorsByName(project.Name, project.Owner, 100);
                     project.Contributors = response;
                 });
                await Task.WhenAll(tasks);
                result.AddRange(temp);
            }
            _logger.LogInformation("{Origin}: Returning projects with the topic: {name}.",
                this, topic);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " in {origin} with request: \"{name}\"", this, topic);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }

    }
    
    //todo: add contributors to the result
    /// <summary>
    /// GetByName gets a repository based on it's name and ownerName
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">repository owner name</param>
    /// <returns>A single repository in the form of ProjectDto</returns>
    public async Task<ProjectDto> GetByName(string name, string ownerName)
    {
        var result = await _gitHubGraphqlService.QueryRepositoryByName(name, ownerName);        
        _logger.LogInformation("{Origin}: Returning repository {name} owned by: {owner}.",
            this, name , ownerName);
        return _graphqlDataConverter.RepositoryToProject(result.Repository);
    }
    
    //todo: add contributors to the result
    /// <summary>
    /// GetByNames gets repositories by their name and ownerNames
    /// </summary>
    /// <param name="repos">A list of projectRequestDtos with at least a repoName and ownerName</param>
    /// <returns>Returns the list of requested repositories in the form of List&lt;ProjectDto&gt;</returns>
    public async Task<List<ProjectDto>> GetByNames(List<ProjectRequestDto> repos)
    {
        var result = _graphqlDataConverter.SearchToProjects(await _gitHubGraphqlService
            .ToQueryString(repos));
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
    
    //todo: add contributors to the result
    /// <summary>
    /// Get ContributorsByName gets the contributors of a repository based on the repositories name and ownerName
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">Repository owner name</param>
    /// <param name="amount">Amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?. Returns null if there are no
    /// contributors or they cannot be accessed</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ContributorDto>?> GetContributorsByName(string name, string ownerName, int amount)
    {
        try
        {
            var result = await _gitHubRestService.GetRepoContributors(ownerName,
                name, amount);     
            return result;
        }
        catch (Exception e)
        {
             _logger.LogError(e.Message + " in {origin} with request: \"{name}/{ownerName}\"", this,
                 name, ownerName);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }
    }
}