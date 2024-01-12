namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Interface for a scheduler that is responsible for scheduling jobs.
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// Adds a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="amount"></param>
    /// <param name="miningFrequency"></param>
    /// <returns></returns>
    public void AddRecurringTopicMiningJob(string topic, int amount, MiningFrequency miningFrequency);
    
    public void AddRecurringKeywordMiningJob(string keyword, int amount, MiningFrequency miningFrequency);
    public void RemoveRecurringTopicMiningJob(string topic);
    
    public void RemoveRecurringKeywordMiningJob(string keyword);
    
}