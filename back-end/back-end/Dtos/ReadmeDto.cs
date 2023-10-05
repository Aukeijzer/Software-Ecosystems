using System.Runtime.Serialization;

namespace SECODashBackend.Dtos;

/// <summary>
/// Data Transfer Object for a readme
/// </summary>
[DataContract]
public class ReadmeDto
{
    [DataMember(Name = "projectId")]
    public required string ProjectId { get; init; }
    
    [DataMember(Name = "readme")]
    public required string Readme { get; init; }
}