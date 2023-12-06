using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for a sub-ecosystem.
/// </summary>
public class SubEcosystemDto
{
    [DataMember(Name = "topic")] public required string Topic { get; init; }
    [DataMember(Name = "projectCount")] public int ProjectCount { get; init; }
}