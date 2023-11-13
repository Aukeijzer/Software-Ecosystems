using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IProjectsService _projectsService;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    {
        _logger = logger;
        _projectsService = projectsService;
    }
    
    [HttpPost("mine/topic")]
    public async Task<ActionResult> MineByTopic(string topic, int amount)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await _projectsService.MineByTopicAsync(topic, amount);
        return Accepted();
    }
    
    [HttpPost("mine/search")]
    public async Task<ActionResult> MineByKeyword(string keyword, int amount)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{keyword}'.", this,keyword);
        await _projectsService.MineByKeywordAsync(keyword, amount);
        return Accepted();
    }
    
    [HttpPost("mine/topic")]
    public async Task<ActionResult> MineByTopic(string topic, int amount)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await _projectsService.MineByTopicAsync(topic, amount);
        return Accepted();
    }
    
    [HttpPost("mine/search")]
    public async Task<ActionResult> MineByKeyword(string keyword, int amount)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{keyword}'.", this,keyword);
        await _projectsService.MineByKeywordAsync(keyword, amount);
        return Accepted();
    }
}