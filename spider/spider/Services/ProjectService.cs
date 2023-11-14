using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using spider.Controllers;
using spider.Converter;
using spider.Dtos;

namespace spider.Services;

public class ProjectService : IProjectService
{
    private readonly ILogger<SpiderController> _logger;
    private readonly IGitHubGraphqlService _gitHubGraphqlService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;
    private readonly IGitHubRestService _gitHubRestService;
    
    public ProjectService(IGitHubGraphqlService gitHubGraphqlService, IGraphqlDataConverter graphqlDataConverter,
        IGitHubRestService gitHubRestService, ILogger<SpiderController> logger)
    {
        _logger = logger;
        _gitHubGraphqlService = gitHubGraphqlService;
        _graphqlDataConverter = graphqlDataConverter;
        _gitHubRestService = gitHubRestService;
    }
    
    public async Task<ActionResult<List<ProjectDto>>> GetByKeyword(string name, int amount, string? startCursor)
    {
        name = WebUtility.UrlDecode(name);
        if (startCursor != null)
        {
            WebUtility.UrlDecode(startCursor);
        }
        _logger.LogInformation("{Origin}: Project requested by name: {name}.", this, name);
        try
        {
            var listResult = await _gitHubGraphqlService.QueryRepositoriesByNameHelper(name, amount, startCursor);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var spiderData in listResult)
            {
                result.AddRange(_graphqlDataConverter.SearchToProjects(spiderData));
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
    
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount, string? startCursor)
    {
        try
        {
            topic = WebUtility.UrlDecode(topic);
            if (startCursor != null)
            {
                WebUtility.UrlDecode(startCursor);
            }

            var listResult = await _gitHubGraphqlService.QueryRepositoriesByTopicHelper(topic, amount, startCursor);
            _logger.LogInformation("{Origin}: Projects requested by topic: {name}.", this, topic);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var topicSearchData in listResult)
            {
                result.AddRange(_graphqlDataConverter.TopicSearchToProjects(topicSearchData));
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

    public async Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Repository requested by name and owner: {name}, {owner}.",
            this, name , ownerName );
        var result = await _gitHubGraphqlService.QueryRepositoryByName(name, ownerName);        
        _logger.LogInformation("{Origin}: Returning repository {name} owned by: {owner}.",
            this, name , ownerName);
        return _graphqlDataConverter.RepositoryToProject(result.Repository);
    }
    
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        _logger.LogInformation("{Origin}: Requested a list of repositories: {repos}.", this, repos);
        var result = _graphqlDataConverter.SearchToProjects(await _gitHubGraphqlService.ToQueryString(repos));
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
    
    public async Task<ActionResult<List<ContributorDto>>> GetContributorsByName(string name, string ownerName, int amount)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Contributors requested by name and owner: {name}, {owner}.",
            this, name , ownerName );
        try
        {
            var result = await _gitHubRestService.GetRepoContributors(name, ownerName, amount);     
            _logger.LogInformation("{Origin}: Returning contributors of repository: {name} owned by: {owner}.",
                this, name , ownerName);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " in {origin} with request: \"{name}/{ownerName}\"", this, name, ownerName);
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