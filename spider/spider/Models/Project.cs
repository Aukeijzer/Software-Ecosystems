using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using spider.Models;

namespace SECODashBackend.Models;

[DataContract]
public class Project
{
   [DataMember(Name = "id")]
   public long? Id { get; set; }
   
   [Required]
   [DataMember(Name = "name")]
   public string Name { get; set; }
   [DataMember(Name = "about")]
   public string? About { get; set; }
   
   [DataMember(Name = "owner")]
   public string? Owner { get; set; }
   
   [DataMember(Name = "description")]
   public string? Description { get; set; }
   
   [DataMember(Name = "Topics")]
   public string[]? Topics { get; set; }

   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }
}