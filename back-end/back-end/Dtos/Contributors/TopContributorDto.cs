using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Contributors;

/// <summary>
/// Represents a data transfer object for a Top Contributor of an ecosystem.
/// </summary>
public class TopContributorDto
{
    [DataMember(Name = "login")] public required string Login { get; init; }
    [DataMember(Name = "contributions")] public int Contributions { get; init; }
}