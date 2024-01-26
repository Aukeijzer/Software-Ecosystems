using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Topic;

/// <summary>
/// Represents a data transfer object for a topic returned by the Data Processor.
/// </summary>
public class TopicResponseDto
{
    [DataMember(Name ="projectId")]
    public required string ProjectId { get; init; }
    
    [DataMember(Name ="topics")]
    public List<string> Topics { get; init; }
}