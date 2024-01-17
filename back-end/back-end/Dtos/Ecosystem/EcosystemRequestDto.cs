using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for an Ecosystem request by the front-end.
/// </summary>
public class EcosystemRequestDto
{
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "technologies")] public required List<string> Technologies { get; init; }
    [DataMember(Name = "numberOfTopLanguages")] public int? NumberOfTopLanguages { get; set; }
    [DataMember(Name = "numberOfSubEcosystems")] public int? NumberOfTopSubEcosystems { get; set; }
    [DataMember(Name = "numberOfTopContributors")] public int? NumberOfTopContributors { get; set; }
}