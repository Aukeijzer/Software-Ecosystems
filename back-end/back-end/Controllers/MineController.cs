using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Scheduler;

namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to mining projects.
/// </summary>
/// <param name="logger"></param>
/// <param name="projectsService"></param>
[ApiController]
[Route("[controller]")]
public class MineController(
    ILogger<MineController> logger,
    IProjectsService projectsService,
    IScheduler scheduler)
    : ControllerBase
{
    /// <summary>
    /// This method returns a list of projects based on the given topic and amount.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    [HttpPost("topic")]
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
    [HttpPost("search")]
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
    /// <returns></returns>
    [HttpPost("taxonomy")]
    public async Task<ActionResult> MineByTaxonomy(List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        logger.LogInformation("{Origin}: Mining command received for taxonomy.", this);
        await projectsService.MineByTaxonomyAsync(taxonomy, keywordAmount, topicAmount);
        return Accepted();
    }
    
    [HttpGet("schedule/topic")]
    public IActionResult ScheduleMineByTopic(string topic, int amount, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringTopicMiningJob(topic, amount, miningFrequency); 
        return Ok($"Mining job for topic: {topic} with amount: {amount} and frequency: {miningFrequency} scheduled.");
    }

    [HttpGet("unschedule/topic")]
    public IActionResult UnscheduleMineByTopic(string topic)
    {
        scheduler.RemoveRecurringTopicMiningJob(topic);
        return Ok($"Mining job for topic: {topic} unscheduled if it existed.");
    }
    
    [HttpGet("schedule/keyword")]
    public IActionResult ScheduleMineByKeyword(string keyword, int amount, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringKeywordMiningJob(keyword, amount, miningFrequency); 
        return Ok($"Mining job for keyword: {keyword} with amount: {amount} and frequency: {miningFrequency} scheduled.");
    }
    
    [HttpGet("unschedule/keyword")]
    public IActionResult UnscheduleMineByKeyword(string keyword)
    {
        scheduler.RemoveRecurringKeywordMiningJob(keyword);
        return Ok($"Mining job for keyword: {keyword} unscheduled if it existed.");
    }
}