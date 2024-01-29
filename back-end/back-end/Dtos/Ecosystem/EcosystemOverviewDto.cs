using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for the supplementary data of a top level Ecosystem
/// such as Agriculture, Quantum or Artificial Intelligence.
/// </summary>
public class EcosystemOverviewDto
{
    /// <summary>
    /// The name of the ecosystem.
    /// </summary>
    [DataMember(Name = "name")] public required string Name { get; init; }
    
    /// <summary>
    /// The display name of the ecosystem.
    /// </summary>
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    
    /// <summary>
    /// the description of the ecosystem.
    /// </summary>
    [DataMember(Name = "description")] public string? Description { get; set; }
    
    /// <summary>
    /// The total number of projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfProjects")] public long? NumberOfProjects { get; set; }
    
    /// <summary>
    /// The total number of subtopics in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfSubTopics")] public long? NumberOfSubTopics { get; set; }
    
    /// <summary>
    /// The total number of contributions to all projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfContributors")] public long? NumberOfContributors { get; set; }
    
    /// <summary>
    /// The total number of stars of all projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfStars")] public long? NumberOfStars { get; set; }
}