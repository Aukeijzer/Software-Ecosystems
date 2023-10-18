using System.Runtime.Serialization;

namespace spider.Dtos;

[DataContract]
public class ContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "id")]
    public required int Id { get; init; }
    [DataMember(Name = "NodeId")]
    public string? node_id { get; init; }
    [DataMember(Name = "contributions")]
    public int? Contributions { get; init; }
}