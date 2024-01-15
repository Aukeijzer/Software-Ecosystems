namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

/// <summary>
/// Represents a request DTO for updating the description of an ecosystem.
/// </summary>
public class DescriptionRequestDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
    
    [DataMember(Name = "ecosystem")]
    public required string Ecosystem { get; init; }
}