using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;
using SECODashBackend.Records;

namespace SECODashBackend.Services.Analysis;

/// <summary>
/// Interface for services that analyse an ecosystem.
/// </summary>
public interface IAnalysisService
{
    /// <summary>
    /// Analyzes the ecosystem given by the topics.
    /// </summary>
    /// <param name="topics">The topics of the ecosystem.</param>
    /// <param name="excludedTopics">The topics of the ecosystem.</param>
    /// <param name="technologies">The technologies of an ecosystem.</param>
    /// <param name="numberOfTopLanguages">The number of top languages to get.</param>
    /// <param name="numberOfTopSubEcosystems">The number of top sub ecosystems to get.</param>
    /// <param name="numberOfTopContributors">The number of top contributors to get.</param>
    /// <param name="numberOfTopProjects">The number of top projects to retrieve</param>
    /// <param name="numberOfTopTechnologies">The number od top technologies to retrieve.</param>
    /// <param name="startTime">The start date of the period of time to retrieve.</param>
    /// <param name="endTime">The end date of the period of time to retrieve.</param>
    /// <param name="timeBucket">The time frame (in days) we want to use to retrieve projects between the start and end time.</param>
    /// <returns>The result of the analysis.</returns>
    public Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, List<BannedTopic>  excludedTopics, List<Technology> technologies,
        int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors, 
        int numberOfTopTechnologies, int numberOfTopProjects,
        DateTime startTime, DateTime endTime, int timeBucket);
    
    /// <summary>
    /// Gathers the metrics of a top-level/main ecosystem by the projects database for projects that contain the given topics
    /// and aggregating the relevant data from the search response.
    /// </summary>
    /// <param name="topic">The topic of the ecosystem.</param>
    /// <returns>The metrics of the ecosystem.</returns>
    public Task<EcosystemMetrics> GetEcosystemMetricsAsync(string topic);
}