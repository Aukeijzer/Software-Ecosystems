using System.Runtime.Serialization;
 
namespace SECODashBackend.Dtos.TimedData;

public class TimedDataDto
{
    [DataMember(Name = "topic")] public string Topic { get; init; }
    [DataMember(Name = "timeBucket")] public string TimeBucket { get; init; }
    [DataMember(Name = "projectCount")] public long ProjectCount { get; init; }
}