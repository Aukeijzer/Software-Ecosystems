using System.Runtime.Serialization;
namespace SECODashBackend.Dtos.Ecosystem;

public class TimedDateDto
{
    [DataMember(Name = "topic")] public string Topic { get; init; }
    [DataMember(Name = "timeBucket")] public DateTime TimeBucket { get; init; }
    [DataMember(Name = "projectCount")] public int ProjectCount { get; init; }
}