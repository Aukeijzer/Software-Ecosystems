using System.Runtime.Serialization;

namespace spider.Dtos;

[DataContract]
public class ContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "id")]
    public required int Id { get; init; }
    [DataMember(Name = "nodeId")]
    public required string NodeId { get; init; }
    [DataMember(Name = "contributions")]
    public required int Contributions { get; init; }
}