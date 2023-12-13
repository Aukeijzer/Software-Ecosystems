using System.Runtime.Serialization;
namespace SECODashBackend.Dtos.Ecosystem;

public class TimedDateDto
{
    [DataMember(Name = "date")] public required string? Date { get; init; }
    [DataMember(Name = "projectCount")] public int ProjectCount { get; init; }
}