using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;

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
    public Task<long> GetByTimeFrameAsync(DateTime startTime, DateTime endTime, string topic);
    /// <summary>
    /// Requests the Spider for projects related to the given taxonomy and saves them to the database.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    public Task MineByTaxonomy(List<string> taxonomy, int keywordAmount, int topicAmount);
}