namespace SECODashBackend.Services.Projects;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects and saving them to the database.
/// </summary>
public interface IProjectsService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic and saves them to the database.
    /// </summary>
    /// <param name="topic">The topic to to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task MineByTopicAsync(string topic, int amount);
    /// <summary>
    /// Requests the Spider for projects related to the given keyword and saves them to the database.
    /// </summary>
    /// <param name="keyword">The keyword to to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task MineByKeywordAsync(string topic, int amount);
    public Task MineByTaxonomyAsync(List<string> taxonomy, int keywordAmount, int topicAmount);
}