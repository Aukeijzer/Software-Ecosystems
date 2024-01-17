using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Project;

public class TopProjectDto
{
   [DataMember(Name = "name")] public required string Name { get; set; }
   [DataMember(Name = "owner")] public required string Owner { get; set; }
   [DataMember(Name = "numberOfStars")] public int NumberOfStars { get; set; }
}