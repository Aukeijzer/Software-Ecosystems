using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for an Ecosystem request by the front-end.
/// </summary>
public class EcosystemRequestDto
{
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "languages")] public List<string>? Languages { get; init; }
    [DataMember(Name = "numberOfTopLanguages")] public int? NumberOfTopLanguages { get; set; }
    [DataMember(Name = "numberOfSubEcosystems")] public int? NumberOfTopSubEcosystems { get; set; }
    [DataMember(Name = "numberOfTopContributors")] public int? NumberOfTopContributors { get; set; }
    [DataMember(Name = "numberOfTopTechnologies")] public int? NumberOfTopTechnologies { get; set; }
    [DataMember(Name = "numberOfTopProjects")] public int? NumberOfTopProjects { get; set; }
    [DataMember(Name = "startTime")] public DateTime StartTime { get; set; }
    [DataMember(Name = "endTime")] public DateTime EndTime { get; set; }
    [DataMember(Name = "numbersOfDaysPerBucket")] public int? NumbersOfDaysPerBucket { get; set; }
}