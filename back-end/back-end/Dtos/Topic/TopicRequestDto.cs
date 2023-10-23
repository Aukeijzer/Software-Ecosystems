using System.Runtime.Serialization;

namespace SECODashBackend.Dtos;

/// <summary>
/// Data Transfer Object for a readme
/// </summary>
[DataContract]
public class TopicRequestDto
{
    [DataMember(Name = "id")]
    public required string Id { get; init; }
    
    [DataMember(Name = "name")]
    public required string Name { get; init; }
    
    [DataMember(Name = "description")] 
    public string? Description { get; set; }
    
    [DataMember(Name = "readme")]
    public required string Readme { get; init; }
}