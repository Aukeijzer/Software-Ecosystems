using System.Runtime.Serialization;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Dtos.TimedData;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// A data transfer object for an Ecosystem with all its supplementary data.
/// </summary>
public class EcosystemDto
{
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    [DataMember(Name = "description")] public string? Description { get; set; }
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "topTechnologies")] public required List<SubEcosystemDto> TopTechnologies { get; init; }
    [DataMember(Name = "topLanguages")] public required List<ProgrammingLanguageDto> TopLanguages { get; init; }
    [DataMember(Name = "subEcosystems")] public required List<SubEcosystemDto> TopSubEcosystems { get; init; }
    [DataMember(Name = "topContributors")] public required List<TopContributorDto> TopContributors { get; init; }
    [DataMember(Name = "topProjects")] public required List<TopProjectDto> TopProjects { get; init; }
    [DataMember(Name = "numberOfStars")] public long NumberOfStars { get; set; }
    [DataMember(Name = "numberOfProjects")] public long NumberOfProjects { get; set; }
    [DataMember(Name = "numberOfTopics")] public long NumberOfTopics { get; set; }
    [DataMember(Name = "numberOfContributors")] public long NumberOfContributors { get; set; }
    [DataMember(Name = "numberOfContributions")] public long NumberOfContributions { get; set; }
    /// <summary>
    /// The number of active projects in the sub ecosystems over time
    /// </summary>
    [DataMember(Name = "topicsActivityTimeSeries")] public List<TopicsBucketDto>? TopicsActivityTimeSeries { get; set; }
    /// <summary>
    /// The number of active projects in the ecosystem over time
    /// </summary>
    [DataMember(Name = "ecosystemActivityTimeSeries")] public List<TopicsBucketDto>? EcosystemActivityTimeSeries { get; set; }
}
   