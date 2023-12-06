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
    /// <param name="topic"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
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
    /// <param name="keyword"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
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
    /// <param name="time"></param>
    /// <returns></returns>
    [HttpPost("searchbytimeframe")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTimeFrameAsync(DateTime time)
    {
        logger.LogInformation("{Origin}: Projects requested with time: '{time}' '.", this, time);
        await projectsService.GetByTimeFrameAsync(time);
        return Accepted();
    }
}