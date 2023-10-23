using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;

namespace SECODashBackend.Dtos.Ecosystem;

public class EcosystemDto
{
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [DataMember(Name = "name")]
   public required string Name { get; init; }
   
   [DataMember(Name = "displayName")]
   public string? DisplayName { get; init; }
   
   [DataMember(Name ="description")]
   public string? Description { get; init; }

   [Required]
   [DataMember(Name = "projects")]
   public IEnumerable<ProjectDto>? Projects { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; init; }
   
   [DataMember(Name = "topLanguages")]
   public List<EcosystemProgrammingLanguage> TopLanguages { get; set; } = new();
}