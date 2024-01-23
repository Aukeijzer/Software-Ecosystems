using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for a sub-ecosystem.
/// </summary>
public class SubEcosystemDto
{
    [DataMember(Name = "topic")] public required string Topic { get; init; }
    [DataMember(Name = "projectCount")] public required long ProjectCount { get; init; }

    public override bool Equals(object? obj)
    {
        return Topic == ((SubEcosystemDto) obj!).Topic && ProjectCount == ((SubEcosystemDto) obj).ProjectCount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Topic, ProjectCount);
    }
}