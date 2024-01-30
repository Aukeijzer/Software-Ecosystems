﻿using Hangfire;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Implementation of <see cref="IScheduler"/> using Hangfire.
/// </summary>
public class HangfireScheduler(
    ILogger<HangfireScheduler> logger,
    IRecurringJobManager recurringJobManager,
    IProjectsService projectsService) : IScheduler
{
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic to mine by</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringTopicMiningJob(string topic, string ecosystem, int amount, MiningFrequency miningFrequency)
    {
        var jobId = $"topic-mining_topic={topic}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByTopicAsync(topic, ecosystem, amount), GetCronFrequency(miningFrequency));
        logger.LogInformation($"Job Id: {jobId} added.");
    }
    
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword to mine by. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringKeywordMiningJob(string keyword, string ecosystem, int amount, MiningFrequency miningFrequency)
    {
        var jobId = $"keyword-mining_keyword={keyword}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByKeywordAsync(keyword, ecosystem, amount), GetCronFrequency(miningFrequency));
        logger.LogInformation($"Job Id: {jobId} added.");
    }
    
    /// <summary>
    /// Removes a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic belonging to the job that is to removed. </param>
    public void RemoveRecurringTopicMiningJob(string topic)
    {
        var jobId = $"topic-mining_topic={topic}";
        recurringJobManager.RemoveIfExists(jobId);
        logger.LogInformation($"Job Id: {jobId} removed if it existed.");
    }

    /// <summary>
    /// Removes a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword belonging to the job that is to removed. </param>
    public void RemoveRecurringKeywordMiningJob(string keyword)
    {
       var jobId = $"keyword-mining_keyword={keyword}";
       recurringJobManager.RemoveIfExists(jobId);
       logger.LogInformation($"Job Id: {jobId} removed if it existed.");
    }

    /// <summary>
    /// Add a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystemName"> The name of the ecosystem. </param>
    /// <param name="taxonomy"> The taxonomy to mine by. </param>
    /// <param name="keywordAmount"> The amount of projects to mine for each term using keyword search. </param>
    /// <param name="topicAmount"> The amount of projects to mine for each term using topic search. </param>
    /// <param name="day">Zero indexed day of the week.</param>
    public void AddRecurringTaxonomyMiningJob(string ecosystemName, List<string> taxonomy, int keywordAmount, int topicAmount, DayOfWeek day)
    {
        var jobId = $"taxonomy-mining_ecosystem={ecosystemName}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByTaxonomyAsync(taxonomy, ecosystemName, keywordAmount, topicAmount), Cron.Weekly(day));
        logger.LogInformation($"Job Id: {jobId} added.");
    }

    /// <summary>
    /// Removes a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystem"> The name of the ecosystem that the job belongs to.</param>
    public void RemoveRecurringTaxonomyMiningJob(string ecosystem)
    {
        var jobId = $"taxonomy-mining_ecosystem={ecosystem}";
        recurringJobManager.RemoveIfExists(jobId);
        logger.LogInformation($"Job Id: {jobId} removed if it existed.");
    }

    /// <summary>
    /// Gets the cron frequency of the job based on the given <see cref="MiningFrequency"/>.
    /// </summary>
    /// <param name="miningFrequency"> The mining frequency for which the Cron frequency is requested.</param>
    /// <returns> The corresponding Cron frequency.</returns>
    private static string GetCronFrequency(MiningFrequency miningFrequency)
    {
        return miningFrequency switch
        {
            MiningFrequency.Hourly => Cron.Hourly(),
            MiningFrequency.Daily => Cron.Daily(),
            MiningFrequency.Weekly => Cron.Weekly(),
            _ => throw new ArgumentOutOfRangeException(nameof(miningFrequency), miningFrequency, null)
        };
    }
}