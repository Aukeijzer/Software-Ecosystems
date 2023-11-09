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
    
    [HttpPost("mine")]
    public async Task<ActionResult> MineByTopic(string topic)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await _projectsService.MineByTopicAsync(topic);
        return Accepted();
    }
}