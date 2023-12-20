namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Interface for a service that is responsible for scheduling jobs.
/// </summary>
public interface ISchedulerService
{
    public string AddRecurringTopicMiningJob(string topic, int amount, MiningFrequency miningFrequency);
}