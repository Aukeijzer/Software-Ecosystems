namespace SECODashBackend.Services.Scheduler;

/// <summary>
/// Interface for a scheduler that is responsible for scheduling jobs.
/// </summary>
public interface IScheduler
{
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic to mine by</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringTopicMiningJob(string topic, string ecosystem, int amount, MiningFrequency miningFrequency);
    /// <summary>
    /// Adds or updates a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword to mine by. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount"> The amount of projects to mine. </param>
    /// <param name="miningFrequency"> The frequency of mining. </param>
    public void AddOrUpdateRecurringKeywordMiningJob(string keyword, string ecosystem, int amount, MiningFrequency miningFrequency);
    /// <summary>
    /// Removes a recurring job that mines projects by a topic.
    /// </summary>
    /// <param name="topic"> The topic belonging to the job that is to removed. </param>
    public void RemoveRecurringTopicMiningJob(string topic);
    /// <summary>
    /// Removes a recurring job that mines projects by a keyword.
    /// </summary>
    /// <param name="keyword"> The keyword belonging to the job that is to removed. </param>
    public void RemoveRecurringKeywordMiningJob(string keyword);

    /// <summary>
    /// Add a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystemName"> The name of the ecosystem. </param>
    /// <param name="taxonomy"> The taxonomy to mine by. </param>
    /// <param name="keywordAmount"> The amount of projects to mine for each term using keyword search. </param>
    /// <param name="topicAmount"> The amount of projects to mine for each term using topic search. </param>
    /// <param name="day">Zero indexed day of the week.</param>
    public void AddRecurringTaxonomyMiningJob(string ecosystemName, List<string> taxonomy, int keywordAmount, int topicAmount, DayOfWeek day);
    /// <summary>
    /// Removes a recurring job that mines projects by a taxonomy.
    /// </summary>
    /// <param name="ecosystem"> The ecosystem belonging to the job that is to removed. </param>
    public void RemoveRecurringTaxonomyMiningJob(string ecosystem);
}