using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using spider.Models;

namespace spider.Dtos;

[DataContract]
public class ProjectDto
{
   [Required]
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [Required]
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   
   [DataMember(Name = "owner")]
   public string? Owner { get; set; }
   
   [DataMember(Name = "description")]
   public string? Description { get; set; }
   
   [DataMember(Name = "topics")]
   public string[]? Topics { get; set; }
   
   [DataMember(Name = "languages")]
   public ProgrammingLanguage[]? Languages { get; set; }
   
   [DataMember(Name = "totalSize")]
   public int? TotalSize { get; set; }

   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }

}
