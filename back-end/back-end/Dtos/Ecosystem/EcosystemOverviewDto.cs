using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Ecosystem;

public class EcosystemOverviewDto
{
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    [DataMember(Name = "description")] public string? Description { get; set; }
    [DataMember(Name = "numberOfStars")] public int? NumberOfStars { get; set; }
}