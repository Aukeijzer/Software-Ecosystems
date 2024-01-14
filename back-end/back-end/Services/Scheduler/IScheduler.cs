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
    
    /// <summary>
    /// Add a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystemName"> The name of the ecosystem. </param>
    /// <param name="taxonomy"> The taxonomy to mine by. </param>
    /// <param name="keywordAmount"> The amount of projects to mine for each term using keyword search. </param>
    /// <param name="topicAmount"> The amount of projects to mine for each term using topic search. </param>
    /// <returns></returns>
    public void AddRecurringTaxonomyMiningJob(string ecosystemName, List<string> taxonomy, int keywordAmount, int topicAmount);
    /// <summary>
    /// Removes a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystem"> The ecosystem belonging to the job that is to removed. </param>
    /// <returns></returns>
    public void RemoveRecurringTaxonomyMiningJob(string ecosystem);
}