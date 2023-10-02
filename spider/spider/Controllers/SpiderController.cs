using Microsoft.AspNetCore.Mvc;
using spider.Converter;
using spider.Models;
using spider.Services;

namespace spider.Controllers;

[ApiController]
[Route("[controller]")]
public class SpiderController : ControllerBase
{
    private readonly IGitHubService _gitHubService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;

    public SpiderController(IGitHubService gitHubService, IGraphqlDataConverter graphqlDataConverter)
    {
        _gitHubService = gitHubService;
        _graphqlDataConverter = graphqlDataConverter;
    }
    //http:localhost:Portnumberhere/spider/name
    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<Project>>> GetByName(string name)
    {
        return _graphqlDataConverter.SearchToProjects(await _gitHubService.QueryRepositoriesByName(name));
    }
    
    [HttpGet("topic/{topic}")]
    public async Task<ActionResult<List<Project>>> GetByTopic(string topic)
    {
        var result = await _gitHubService.QueryRepositoriesByTopic(topic);
        return (result.topic == null) ? new BadRequestResult() :  _graphqlDataConverter.TopicSearchToProjects(result);
    }
    
    [HttpGet("repository/{name}/{ownerName}")]
    public async Task<ActionResult<Project>> GetByName(string name, string ownerName)
    {
        var result = await _gitHubService.QueryRepositoryByName(name, ownerName);
        return _graphqlDataConverter.RepositoryToProject(result.repository);
    }
}