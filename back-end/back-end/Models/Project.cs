using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SECODashBackend.Models;

[DataContract]
public class Project
{
   [DataMember(Name = "id")]
   public long? Id { get; set; }
   
   [Required]
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   [DataMember(Name = "about")]
   public string? About { get; set; }
   
   [DataMember(Name = "owner")]
   public string? Owner { get; set; }
   
   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }
}