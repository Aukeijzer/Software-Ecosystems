using System.Net;
using Microsoft.AspNetCore.Mvc;
using spider.Dtos;
using spider.Services;

namespace spider.Controllers;

[ApiController]
[Route("[controller]")]
public class SpiderController : ControllerBase
{
    private readonly ILogger<SpiderController> _logger;
    private readonly ISpiderProjectService _spiderProjectService;

    public SpiderController(ILogger<SpiderController> logger, ISpiderProjectService spiderProjectService)
    {
        _logger = logger;
        _spiderProjectService = spiderProjectService;
    }
    //http:localhost:Portnumberhere/spider/name/amount
    [HttpGet("name/{name}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByKeyword(string name, int amount)
    {
        return await GetByKeyword(name, amount, null);
    }

    [HttpGet("name/{name}/{amount}/{startCursor}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByKeyword(string name, int amount, string? startCursor)
    {
        name = WebUtility.UrlDecode(name);
        if (startCursor != null)
        {
            startCursor = WebUtility.UrlDecode(startCursor);
        }
        _logger.LogInformation("{Origin}: Project requested by name: {name}.", this, name);
        return await _spiderProjectService.GetByKeyword(name, amount, startCursor);
    }
    
    [HttpGet("topic/{topic}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount)
    {
        return await GetByTopic(topic, amount, null);
    }

    [HttpGet("topic/{topic}/{amount}/{startCursor}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount, string? startCursor)
    {
        topic = WebUtility.UrlDecode(topic);
        if (startCursor != null)
        {
            startCursor = WebUtility.UrlDecode(startCursor);
        }
        _logger.LogInformation("{Origin}: Projects requested by topic: {name}.", this, topic);
        return await _spiderProjectService.GetByTopic(topic, amount, startCursor);
    }
    
    [HttpGet("repository/{name}/{ownerName}")]
    public async Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Repository requested by name and owner: {name}, {owner}.", 
            this, name , ownerName );
        return await _spiderProjectService.GetByName(name, ownerName);
    }

    [HttpPost]
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        _logger.LogInformation("{Origin}: Requested a list of repositories: {repos}.", this, repos);
        return await _spiderProjectService.GetByNames(repos);
    }
    
    [HttpGet("Contributors/{ownerName}/{name}/{amount}")]
    public async Task<ActionResult<List<ContributorDto>>> GetContributorsByName(string name, string ownerName,
        int amount)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Contributors requested by name and owner: {name}, {owner}.",
            this, name , ownerName );
        var result = await _spiderProjectService.GetContributorsByName(name, ownerName, amount);
        _logger.LogInformation("{Origin}: Returning contributors of repository: {name} owned by: {owner}.",
            this, name , ownerName);
        if (result == null)
        {
            return new List<ContributorDto>();
        }
        return result;
    }
}