using System.Runtime.Serialization;

namespace spider.Dtos;

/// <summary>
/// A data transfer object for a contributor to a repository
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