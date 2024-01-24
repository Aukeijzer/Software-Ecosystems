using System.Runtime.Serialization;
using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Dtos.TimedData;

/// <summary>
/// Represents a data transfer object for a bucket that contains topic data for a given time period.
/// </summary>
[DataContract]
public class TopicsBucketDto
{
    [DataMember(Name = "bucketDateLabel")] public string DateLabel { get; init; }
    [DataMember(Name = "topics")] public List<SubEcosystemDto> Topics { get; init; }
}