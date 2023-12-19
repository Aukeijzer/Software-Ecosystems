namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

public class descriptionRequestDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
    
    [DataMember(Name = "ecosystem")]
    public required string Ecosystem { get; init; }
}