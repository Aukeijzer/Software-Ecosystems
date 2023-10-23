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
    private readonly IGitHubGraphqlService _gitHubGraphqlService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;
    private readonly IGithubRestService _githubRestService;

    public SpiderController(IGitHubGraphqlService gitHubGraphqlService, IGraphqlDataConverter graphqlDataConverter,
        IGithubRestService githubRestService, ILogger<SpiderController> logger)
    {
        _logger = logger;
        _gitHubGraphqlService = gitHubGraphqlService;
        _graphqlDataConverter = graphqlDataConverter;
        _githubRestService = githubRestService;
    }
    //http:localhost:Portnumberhere/spider/name
    [HttpGet("name/{name}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByName(string name, int amount, string? startCursor = null)
    {
        name = WebUtility.UrlDecode(name);
        if (startCursor != null)
        {
            WebUtility.UrlDecode(startCursor);
        }
        _logger.LogInformation("{Origin}: Project requested by name: {name}.", this, name );
        var listResult = await _gitHubGraphqlService.QueryRepositoriesByNameHelper(name, amount, startCursor);
        List<ProjectDto> result = new List<ProjectDto>();
        foreach (var spiderData in listResult)
        {
            result = result.Concat(_graphqlDataConverter.SearchToProjects(spiderData)).ToList();
        }
        
        _logger.LogInformation("{Origin}: Returning the project with name: {name}.", this, name);
        return result;
    }
    
    [HttpGet("topic/{topic}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount)
    {
        topic = WebUtility.UrlDecode(topic);
        _logger.LogInformation("{Origin}: Projects requested by topic: {name}.", this, topic );
        var result = await _gitHubGraphqlService.QueryRepositoriesByTopic(topic);
        _logger.LogInformation("{Origin}: Returning projects with the topic: {name}.", this, topic);
        return (result.Topic == null) ? new BadRequestResult() :  _graphqlDataConverter.TopicSearchToProjects(result);
    }
    
    [HttpGet("repository/{name}/{ownerName}")]
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

    [HttpPost]
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        _logger.LogInformation("{Origin}: Requested a list of repositories: {repos}.", this, repos);
        var result = _graphqlDataConverter.SearchToProjects(await _gitHubGraphqlService.ToQueryString(repos));
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
    
    [HttpGet("Contributors/{ownerName}/{name}/{amount}")]
    public async Task<ActionResult<List<ContributorDto>>> GetContributorsByName(string name, string ownerName, int amount)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Contributors requested by name and owner: {name}, {owner}.",
            this, name , ownerName );
        var result = await _githubRestService.GetRepoContributors(name, ownerName);        
        _logger.LogInformation("{Origin}: Returning contributors of repository: {name} owned by: {owner}.",
            this, name , ownerName);
        return result;
    }
}