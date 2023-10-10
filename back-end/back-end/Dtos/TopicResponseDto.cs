using System.Runtime.Serialization;

namespace SECODashBackend.Dtos;

public class TopicResponseDto
{
    [DataMember(Name ="projectId")]
    public int ProjectId { get; set; }
    
    [DataMember(Name ="topicId")]
    public int TopicId { get; set; }
    
    [DataMember(Name = "label")]
    public string? Label { get; set; }
    
    [DataMember(Name = "keywords")]
    public required List<string> Keywords { get; init; }
    
    [DataMember(Name = "probability")]
    public required float Probability { get; init; }
}