using System.Net;
using System.Text.Json;
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
    private readonly IProjectService _projectService;

    public SpiderController(ILogger<SpiderController> logger, IProjectService projectService)
    {
        _logger = logger;
        _projectService = projectService;
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
        return await _projectService.GetByKeyword(name, amount, startCursor);
    }
    
    [HttpGet("topic/{topic}/{amount}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount)
    {
        return await GetByTopic(topic, amount, null);
    }

    [HttpGet("topic/{topic}/{amount}/{startCursor}")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount, string? startCursor)
    {
        return await _projectService.GetByTopic(topic, amount, startCursor);
    }

    [HttpGet("repository/{name}/{ownerName}")]
    public async Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName)
    {
        return await _projectService.GetByName(name, ownerName);
    }

    [HttpPost]
    public async Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos)
    {
        return await _projectService.GetByNames(repos);
    }
    
    [HttpGet("Contributors/{ownerName}/{name}/{amount}")]
    public async Task<ActionResult<List<ContributorDto>>> GetContributorsByName(string name, string ownerName,
        int amount)
    {
        return await _projectService.GetContributorsByName(name, ownerName, amount);
    }
}