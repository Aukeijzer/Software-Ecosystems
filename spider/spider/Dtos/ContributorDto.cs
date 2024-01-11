using System.Runtime.Serialization;

namespace spider.Dtos;

/// <summary>
/// A data transfer object for a contributor to a repository
/// </summary>
public class ContributorDto
{
    [DataMember] public required string Login { get; init; }
    [DataMember] public required int Id { get; init; }
    [DataMember] public required string NodeId { get; init; }
    [DataMember] public int? Contributions { get; init; }
    [DataMember] public string? Type { get; init; }
}