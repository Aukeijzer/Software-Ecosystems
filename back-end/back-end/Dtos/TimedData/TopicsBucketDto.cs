using System.Runtime.Serialization;
using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Dtos.TimedData;

public class TopicsBucketDto
{
    [DataMember(Name = "bucketDateLabel")] public string BucketDateLabel { get; init; }
    [DataMember(Name = "topics")] public List<SubEcosystemDto> Topics { get; init; }
}