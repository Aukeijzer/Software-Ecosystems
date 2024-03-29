// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

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
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The number of projects to be mined</param>
    [HttpPost("topic")]
    public async Task<ActionResult> MineByTopic(string topic, string ecosystem, int amount)
    {
        logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await projectsService.MineByTopicAsync(topic, ecosystem, amount);
        return Accepted();
    }
    
    /// <summary>
    /// This method returns a list of projects based on the given keyword and amount.
    /// </summary>
    /// <param name="keyword">The keyword that the projects should relate to.</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The number of projects to be mined.</param>
    [HttpPost("search")]
    public async Task<ActionResult> MineByKeyword(string keyword, string ecosystem, int amount)
    {
        logger.LogInformation("{Origin}: Mining command received for topic: '{keyword}'.", this,keyword);
        await projectsService.MineByKeywordAsync(keyword, ecosystem, amount);
        return Accepted();
    }
    
    /// <summary>
    /// This method returns a list of projects based on the given taxonomy and amounts.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    [HttpPost("taxonomy")]
    public async Task<ActionResult> MineByTaxonomy(List<string> taxonomy, string ecosystem, int keywordAmount, int topicAmount)
    {
        logger.LogInformation("{Origin}: Mining command received for taxonomy.", this);
        await projectsService.MineByTaxonomyAsync(taxonomy, ecosystem, keywordAmount, topicAmount);
        return Accepted();
    }
    
    /// <summary>
    /// This method schedules a recurring job that mines projects based on the given topic, amount and frequency.
    /// </summary>
    /// <param name="topic"> The topic to mine by. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    [HttpGet("schedule/topic")]
    public IActionResult ScheduleMineByTopic(string topic, int amount, string ecosystem, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringTopicMiningJob(topic, ecosystem, amount, miningFrequency); 
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
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    [HttpGet("schedule/keyword")]
    public IActionResult ScheduleMineByKeyword(string keyword, string ecosystem, int amount, MiningFrequency miningFrequency)
    {
        scheduler.AddOrUpdateRecurringKeywordMiningJob(keyword, ecosystem, amount, miningFrequency); 
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
    /// <param name="dayOfWeek">Zero indexed day of the week, starting on Sunday.</param>
    [HttpPost("schedule/taxonomy")]
    public IActionResult ScheduleMineByTaxonomy(string ecosystem, List<string> taxonomy, int keywordAmount, int topicAmount, DayOfWeek dayOfWeek)
    {
        scheduler.AddRecurringTaxonomyMiningJob(ecosystem, taxonomy, keywordAmount, topicAmount, dayOfWeek); 
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