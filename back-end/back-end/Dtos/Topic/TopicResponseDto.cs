using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Topic;

public class TopicResponseDto
{
    [DataMember(Name ="projectId")]
    public required string ProjectId { get; init; }
    
    [DataMember(Name ="topicId")]
    public int TopicId { get; init; }
    
    [DataMember(Name = "label")]
    public string? Label { get; init; }
    
    [DataMember(Name = "keywords")]
    public required List<string> Keywords { get; init; }
    
    [DataMember(Name = "probability")]
    public required float Probability { get; init; }
}