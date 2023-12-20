using Hangfire;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Implementation of <see cref="ISchedulerService"/> using Hangfire.
/// </summary>
public class HangfireSchedulerService(
    ILogger<HangfireSchedulerService> logger,
    IRecurringJobManager recurringJobManager,
    IProjectsService projectsService) : ISchedulerService
{

    public string AddRecurringTopicMiningJob(string topic, int amount, MiningFrequency miningFrequency)
    {
        var jobId = $"topic-mining_topic={topic}";
        recurringJobManager.AddOrUpdate(jobId,() => projectsService.MineByTopicAsync("python", 10), GetCronFrequency(miningFrequency));
        logger.LogInformation($"Job Id: {jobId} added...");
        return jobId;
    }
    
    private static string GetCronFrequency(MiningFrequency miningFrequency)
    {
        return miningFrequency switch
        {
            MiningFrequency.Minutely => Cron.Minutely(),
            MiningFrequency.Hourly => Cron.Hourly(),
            MiningFrequency.Daily => Cron.Daily(),
            MiningFrequency.Weekly => Cron.Weekly()
        };
    }
}