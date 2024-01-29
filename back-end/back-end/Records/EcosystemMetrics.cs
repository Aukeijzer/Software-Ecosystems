namespace SECODashBackend.Records;

/// <summary>
/// The metrics of a top-level ecosystem.
/// </summary>
public record EcosystemMetrics
{
    /// <summary>
    /// The total number of projects in the ecosystem.
    /// </summary>
    public long NumberOfProjects { get; init; }
    
    /// <summary>
    /// The total number of subtopics in the ecosystem.
    /// </summary>
    public long NumberOfSubTopics { get; init; }
    
    /// <summary>
    /// The total number of contributors to all projects in the ecosystem.
    /// </summary>
    public long NumberOfContributors { get; init; }
    
    /// <summary>
    /// The total number of stars of all projects in the ecosystem.
    /// </summary>
    public long NumberOfStars { get; init; }
}