using Hangfire;
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
    /// Adds a recurring job that mines projects by topic.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="amount"></param>
    /// <param name="miningFrequency"></param>
    /// <returns></returns>
    public void AddRecurringTopicMiningJob(string topic, int amount, MiningFrequency miningFrequency)
    {
        var jobId = $"topic-mining_topic={topic}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByTopicAsync(topic, amount), GetCronFrequency(miningFrequency));
        logger.LogInformation($"Job Id: {jobId} added.");
    }
    
    public void AddRecurringKeywordMiningJob(string keyword, int amount, MiningFrequency miningFrequency)
    {
        var jobId = $"keyword-mining_keyword={keyword}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByKeywordAsync(keyword, amount), GetCronFrequency(miningFrequency));
        logger.LogInformation($"Job Id: {jobId} added.");
    }
    
    public void RemoveRecurringTopicMiningJob(string topic)
    {
        var jobId = $"topic-mining_topic={topic}";
        recurringJobManager.RemoveIfExists(jobId);
        logger.LogInformation($"Job Id: {jobId} removed if it existed.");
    }

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
    public void AddRecurringTaxonomyMiningJob(string ecosystemName, List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        var jobId = $"taxonomy-mining_ecosystem={ecosystemName}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByTaxonomyAsync(taxonomy, keywordAmount, topicAmount), Cron.Minutely());
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
    /// <param name="miningFrequency"></param>
    /// <returns></returns>
    private static string GetCronFrequency(MiningFrequency miningFrequency)
    {
        return miningFrequency switch
        {
            MiningFrequency.Minutely => Cron.Minutely(),
            MiningFrequency.Hourly => Cron.Hourly(),
            MiningFrequency.Daily => Cron.Daily(),
            MiningFrequency.Weekly => Cron.Weekly(),
            _ => throw new ArgumentOutOfRangeException(nameof(miningFrequency), miningFrequency, null)
        };
    }
}