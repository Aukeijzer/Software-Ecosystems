using System.Runtime.Serialization;

namespace SECODashBackend.Dtos;

/// <summary>
/// Represents a data transfer object for a Contributor of a Project.
/// </summary>
[DataContract]
public class ContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "id")]
    public required int Id { get; init; }
    [DataMember(Name = "nodeId")]
    public string? NodeId { get; init; }
    [DataMember(Name = "contributions")]
    public int? Contributions { get; init; }
    [DataMember(Name = "type")]
    public string? Type { get; init; }
}