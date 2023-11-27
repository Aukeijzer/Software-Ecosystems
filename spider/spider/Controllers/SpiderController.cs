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
    //http:localhost:Portnumberhere/spider/...
    
    /// <summary>
    /// GetByKeyword receives a HttpGet request and returns a list of repositories. This method calls the GetByKeyword
    /// method with a startCursor of null
    /// </summary>
    /// <param name="name">keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <returns>A list of repositories in the form of List&lt;ProjectDto&gt;</returns>
    [HttpGet("name/{name}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByKeyword(string name, int amount)
    {
        return await GetByKeyword(name, amount, null);
    }

    /// <summary>
    /// GetByKeyword receives a HttpGet request and returns a list of repositories
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from. If cursor is null start from the start</param>
    /// <returns>A list of repositories in the form of List&lt;ProjectDto&gt;</returns>
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
    
    /// <summary>
    /// GetByTopic receives a HttpGet request and returns a list of repositories. This method calls the GetByTopic
    /// method with a startCursor of null
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <returns>A list of repositories in the form of List&lt;ProjectDto&gt;</returns>
    [HttpGet("topic/{topic}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount)
    {
        return await GetByTopic(topic, amount, null);
    }

    /// <summary>
    /// GetByTopic receives a HttpGet request and returns a list of repositories
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from. If cursor is null start from the start</param>
    /// <returns>A list of repositories in the form of List&lt;ProjectDto&gt;</returns>
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
    
    /// <summary>
    /// GetByName receives a HttpGet request and returns a repository
    /// </summary>
    /// <param name="name">Name of the repository</param>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <returns>A single repository in the form of ProjectDto</returns>
    [HttpGet("repository/{name}/{ownerName}")]
    public async Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName)
    {
        name = WebUtility.UrlDecode(name);
        ownerName = WebUtility.UrlDecode(ownerName);
        _logger.LogInformation("{Origin}: Repository requested by name and owner: {name}, {owner}.", 
            this, name , ownerName );
        return await _spiderProjectService.GetByName(name, ownerName);
    }

    /// <summary>
    /// GetContributersByName receives a HttpPost request with a list of ProjectRequestDtos and then returns a list of
    /// repositories for each ProjectRequestDto
    /// </summary>
    /// <param name="repos">List of ProjectRequestDtos with at least the repo and ownerNames</param>
    /// <returns>A list of repositories in the form of List&lt;ProjectDto&gt;</returns>
    [HttpPost]
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        _logger.LogInformation("{Origin}: Requested a list of repositories: {repos}.", this, repos);
        return await _spiderProjectService.GetByNames(repos);
    }
    
    /// <summary>
    /// GetContributersByName receives a HttpGet request and returns a list of contributors
    /// </summary>
    /// <param name="name">Name of the repository</param>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <param name="amount">Amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;</returns>
    [HttpGet("Contributors/{name}/{ownerName}/{amount}")]
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