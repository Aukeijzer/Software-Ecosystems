using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Scheduler;

namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to mining projects.
/// </summary>
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
    /// <param name="topic"> The topic that the projects should relate to.</param>
    /// <param name="amount">The number of projects to be mined</param>
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
    /// <param name="keyword">The keyword that the projects should relate to.</param>
    /// <param name="amount">The number of projects to be mined.</param>
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
    [HttpPost("taxonomy")]
    public async Task<ActionResult> MineByTaxonomy(List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        logger.LogInformation("{Origin}: Mining command received for taxonomy.", this);
        await projectsService.MineByTaxonomyAsync(taxonomy, keywordAmount, topicAmount);
        return Accepted();
    }
    
    /// <summary>
    /// This method schedules a recurring job that mines projects based on the given topic, amount and frequency.
    /// </summary>
    /// <param name="topic"> The topic to mine by. </param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    [HttpGet("schedule/topic")]
    public IActionResult ScheduleMineByTopic(string topic, int amount, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringTopicMiningJob(topic, amount, miningFrequency); 
        logger.LogInformation($"{this}: Mining job for topic: {topic} with amount: {amount} and frequency: {miningFrequency} scheduled.");
        return Accepted();
    }

    /// <summary>
    /// This method deletes a recurring job that mines projects based on the given topic if the jobs exists.
    /// </summary>
    /// <param name="topic"> The topic to mine by. </param>
    [HttpGet("unschedule/topic")]
    public IActionResult UnscheduleMineByTopic(string topic)
    {
        scheduler.RemoveRecurringTopicMiningJob(topic);
        logger.LogInformation($"{this}: Mining job for topic: {topic} unscheduled if it existed.");
        return Accepted();
    }
    
    /// <summary>
    /// This method schedules a recurring job that mines projects based on the given keyword, amount and frequency.
    /// </summary>
    /// <param name="keyword"> The keyword to mine by. </param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    [HttpGet("schedule/keyword")]
    public IActionResult ScheduleMineByKeyword(string keyword, int amount, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringKeywordMiningJob(keyword, amount, miningFrequency); 
        logger.LogInformation(
            $"{this}: Mining job for keyword: {keyword} with amount: {amount} and frequency: {miningFrequency} scheduled.");
        return Accepted();
    }
    
    /// <summary>
    /// This method deletes a recurring job that mines projects based on the given keyword if the jobs exists.
    /// </summary>
    /// <param name="keyword"> The keyword to mine by. </param>
    [HttpGet("unschedule/keyword")]
    public IActionResult UnscheduleMineByKeyword(string keyword)
    {
        scheduler.RemoveRecurringKeywordMiningJob(keyword);
        logger.LogInformation($"Mining job for keyword: {keyword} unscheduled if it existed.");
        return Accepted();
    }
    
    /// <summary>
    /// This method schedules a recurring job that mines projects based on the given taxonomy.
    /// </summary>
    /// <param name="ecosystem"> The name of the ecosystem. </param>
    /// <param name="taxonomy"> The taxonomy to mine by. </param>
    /// <param name="keywordAmount"> The amount of projects to mine for each term using keyword search. </param>
    /// <param name="topicAmount"> The amount of projects to mine for each term using topic search. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    [HttpPost("schedule/taxonomy")]
    public IActionResult ScheduleMineByTaxonomy(string ecosystem, List<string> taxonomy, int keywordAmount, int topicAmount, MiningFrequency miningFrequency)
    {
        scheduler.AddRecurringTaxonomyMiningJob(ecosystem, taxonomy, keywordAmount, topicAmount); 
        logger.LogInformation(
            $"Mining job for ecosystem: {ecosystem} using taxonomy: {taxonomy} with keyword amount: {keywordAmount} and topic amount: {topicAmount} scheduled.");
        return Accepted();
    }
    
    /// <summary>
    /// This method deletes a recurring job that mines projects based on the given taxonomy if the job exists.
    /// </summary>
    /// <param name="ecosystem"> The name of the ecosystem. </param>
    [HttpGet("unschedule/taxonomy")]
    public IActionResult UnscheduleMineByTaxonomy(string ecosystem)
    {
        scheduler.RemoveRecurringTaxonomyMiningJob(ecosystem);
        logger.LogInformation($"Mining job for ecosystem: {ecosystem} using a taxonomy unscheduled if it existed.");
        return Accepted();
    }
}