using System.Runtime.Serialization;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for an Ecosystem.
/// </summary>
public class EcosystemDto
{
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    [DataMember(Name = "description")] public string? Description { get; set; }
    [DataMember(Name = "numberOfStars")] public int? NumberOfStars { get; set; }
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "topTechnologies")] public required List<SubEcosystemDto> TopTechnologies { get; init; }
    [DataMember(Name = "topLanguages")] public required List<ProgrammingLanguageDto> TopLanguages { get; init; }
    [DataMember(Name = "subEcosystems")] public required List<SubEcosystemDto> TopSubEcosystems { get; init; }
    [DataMember(Name = "topContributors")] public required List<TopContributorDto> TopContributors { get; init; }
    [DataMember(Name = "topProjects")] public required List<TopProjectDto> TopProjects { get; init; }
    [DataMember(Name = "numberOfProjects")] public long NumberOfProjects { get; set; }
    [DataMember(Name = "numberOfTopics")] public int? NumberOfTopics { get; set; }
    [DataMember(Name = "numberOfContributors")] public int? NumberOfContributors { get; set; }
    [DataMember(Name = "numberOfContributions")] public int? NumberOfContributions { get; set; }
    
    
}
   