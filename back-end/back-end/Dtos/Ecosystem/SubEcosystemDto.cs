using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

public class SubEcosystemDto
{
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "projectCount")] public int ProjectCount { get; init; }
}