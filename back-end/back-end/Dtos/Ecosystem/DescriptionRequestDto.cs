namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

public class DescriptionRequestDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
    
    [DataMember(Name = "ecosystem")]
    public required string Ecosystem { get; init; }
}