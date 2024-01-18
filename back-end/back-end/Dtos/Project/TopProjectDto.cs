using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Project;

/// <summary>
/// Represents a data transfer object for a top project of an ecosystem based on the number of stars.
/// </summary>
[DataContract]
public class TopProjectDto
{
   [DataMember(Name = "name")] public required string Name { get; set; }
   [DataMember(Name = "owner")] public required string Owner { get; set; }
   [DataMember(Name = "numberOfStars")] public int NumberOfStars { get; set; }
}