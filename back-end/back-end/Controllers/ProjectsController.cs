using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to projects.
/// </summary>
/// <param name="logger"></param>
/// <param name="projectsService"></param>
[ApiController]
[Route("[controller]")]
public class ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    : ControllerBase
{
    /// <summary>
    /// This method returns a list of projects based on the given topic and amount.
    /// </summary>
    [HttpPost("mine/topic")]
    public async Task<ActionResult> MineByTopic(string topic, int amount)
    {
        logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await projectsService.MineByTopicAsync(topic, amount);
        return Accepted();
    }
    
    /// <summary>
    /// This method returns a list of projects based on the given keyword and amount.
    /// </summary>
    [HttpPost("mine/search")]
    public async Task<ActionResult> MineByKeyword(string keyword, int amount)
    {
        logger.LogInformation("{Origin}: Mining command received for topic: '{keyword}'.", this,keyword);
        await projectsService.MineByKeywordAsync(keyword, amount);
        return Accepted();
    }
    
    /// <summary>
    /// This method returns a list of projects based on the given time.
    /// </summary>
    [HttpPost("searchbytimeframe")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTimeFrame(DateTime startTime, DateTime endTime, string topic)
    {
        logger.LogInformation("{Origin}: Projects requested with time: '{starttime}' '.", this, startTime);
        var result = await projectsService.GetByTimeFrameAsync(startTime, endTime, topic);
        return new ObjectResult(result);
    }
    
}