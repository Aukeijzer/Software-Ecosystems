using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;

[Index(nameof(Name), IsUnique = true)]
public class Ecosystem
{  
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   
   [DataMember(Name = "displayName")]
   public string? DisplayName { get; set; }
   
   [DataMember(Name ="description")]
   public string? Description { get; set; }

   [DataMember(Name = "projects")] 
   public List<Project> Projects { get; set; } = new();
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }
   
   [DataMember(Name = "topLanguages")]
   public List<EcosystemProgrammingLanguage> TopLanguages { get; set; } = new();
}

