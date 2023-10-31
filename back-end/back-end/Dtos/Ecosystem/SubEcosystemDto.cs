using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

public class SubEcosystemDto
{
    [DataMember(Name = "topic")] public required string Topic { get; init; }
    [DataMember(Name = "projectCount")] public int ProjectCount { get; init; }
}