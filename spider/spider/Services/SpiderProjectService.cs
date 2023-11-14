using System.Net;
using System.Text.Json;
using spider.Controllers;
using spider.Converter;
using spider.Dtos;

namespace spider.Services;

public class SpiderProjectService : ISpiderProjectService
{
    private readonly ILogger<SpiderController> _logger;
    private readonly IGitHubGraphqlService _gitHubGraphqlService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;
    private readonly IGitHubRestService _gitHubRestService;
    
    public SpiderProjectService(IGitHubGraphqlService gitHubGraphqlService, IGraphqlDataConverter graphqlDataConverter,
        IGitHubRestService gitHubRestService, ILogger<SpiderController> logger)
    {
        _logger = logger;
        _gitHubGraphqlService = gitHubGraphqlService;
        _graphqlDataConverter = graphqlDataConverter;
        _gitHubRestService = gitHubRestService;
    }
    
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

    public async Task<ProjectDto> GetByName(string name, string ownerName)
    {
        var result = await _gitHubGraphqlService.QueryRepositoryByName(name, ownerName);        
        _logger.LogInformation("{Origin}: Returning repository {name} owned by: {owner}.",
            this, name , ownerName);
        return _graphqlDataConverter.RepositoryToProject(result.Repository);
    }
    
    public async Task<List<ProjectDto>> GetByNames(List<ProjectRequestDto> repos)
    {
        var result = _graphqlDataConverter.SearchToProjects(await _gitHubGraphqlService
            .ToQueryString(repos));
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
    
    public async Task<List<ContributorDto>?> GetContributorsByName(string name, string ownerName, int amount)
    {
        try
        {
            var result = await _gitHubRestService.GetRepoContributors(name,
                ownerName, amount);     
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