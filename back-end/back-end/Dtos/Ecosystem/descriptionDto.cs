namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

public class descriptionDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
}
