using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to projects.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    : ControllerBase
{
    /// <summary>
    /// This method returns a list of projects based on the given topic and amount.
    /// </summary>
    /// <param name="topic"> The topic that the projects should relate to.</param>
    /// <param name="amount">The number of projects to be mined</param>
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
    /// <param name="keyword">The keyword that the projects should relate to.</param>
    /// <param name="amount">The number of projects to be mined.</param>
    [HttpPost("mine/search")]
    public async Task<ActionResult> MineByKeyword(string keyword, int amount)
    {
        logger.LogInformation("{Origin}: Mining command received for topic: '{keyword}'.", this,keyword);
        await projectsService.MineByKeywordAsync(keyword, amount);
        return Accepted();
    }
    
    /// <summary>
    /// This method returns a list of projects based on the given taxonomy and amounts.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    [HttpPost("mine/taxonomy")]
    public async Task<ActionResult> MineByTaxonomy(List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        logger.LogInformation("{Origin}: Mining command received for taxonomy.", this);
        await projectsService.MineByTaxonomy(taxonomy, keywordAmount, topicAmount);
        return Accepted();
    }
}