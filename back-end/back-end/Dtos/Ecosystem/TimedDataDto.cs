using System.Runtime.Serialization;
using Elastic.Clients.Elasticsearch;

namespace SECODashBackend.Dtos.Ecosystem;

public class TimedDataDto
{
    [DataMember(Name = "topic")] public string Topic { get; init; }
    [DataMember(Name = "timeBucket")] public string TimeBucket { get; init; }
    [DataMember(Name = "projectCount")] public long ProjectCount { get; init; }
}