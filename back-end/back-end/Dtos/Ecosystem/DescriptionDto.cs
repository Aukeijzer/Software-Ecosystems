namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

/// <summary>
/// Represents a data transfer object for the description of an ecosystem.
/// </summary>
public class DescriptionDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
}
