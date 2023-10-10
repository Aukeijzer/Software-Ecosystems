using System.Net;
using Microsoft.AspNetCore.Mvc;
using spider.Converter;
using spider.Dtos;
using spider.Services;

namespace spider.Controllers;

[ApiController]
[Route("[controller]")]
public class SpiderController : ControllerBase
{
    private readonly ILogger<SpiderController> _logger;
    private readonly IGitHubService _gitHubService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;

    public SpiderController(IGitHubService gitHubService, IGraphqlDataConverter graphqlDataConverter, ILogger<SpiderController> logger)
    {
        _logger = logger;
        _gitHubService = gitHubService;
        _graphqlDataConverter = graphqlDataConverter;
    }
    //http:localhost:Portnumberhere/spider/name
    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByName(string name)
    {
        name = WebUtility.UrlDecode(name);
        _logger.LogInformation("{Origin}: Project requested by name: {name}.", this, name );
        var result= _graphqlDataConverter.SearchToProjects(await _gitHubService.QueryRepositoriesByName(name));
        _logger.LogInformation("{Origin}: Returning the project with name: {name}.", this, name);
        return result;
    }
    
    [HttpGet("topic/{topic}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic)
    {
        topic = WebUtility.UrlDecode(topic);
        _logger.LogInformation("{Origin}: Projects requested by topic: {name}.", this, topic );
        var result = await _gitHubService.QueryRepositoriesByTopic(topic);
        _logger.LogInformation("{Origin}: Returning projects with the topic: {name}.", this, topic);
        return (result.Topic == null) ? new BadRequestResult() :  _graphqlDataConverter.TopicSearchToProjects(result);
    }
    
    [HttpGet("repository/{name}/{ownerName}")]
    public async Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Repository requested by name and owner: {name}, {owner}.", this, name , ownerName );
        var result = await _gitHubService.QueryRepositoryByName(name, ownerName);        
        _logger.LogInformation("{Origin}: Returning repository {name} owned by: {owner}.", this, name , ownerName);
        return _graphqlDataConverter.RepositoryToProject(result.Repository);
    }

    [HttpPost]
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        _logger.LogInformation("{Origin}: Requested a list of repositories: {repos}.", this, repos);
        var result = _graphqlDataConverter.SearchToProjects(await _gitHubService.ToQueryString(repos));
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
}