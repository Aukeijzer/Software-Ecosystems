namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

public class DescriptionDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
}
