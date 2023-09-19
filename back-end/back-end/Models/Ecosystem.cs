using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace SECODashBackend.Models;

[DataContract]
public class Ecosystem
{

   [DataMember(Name = "id")]
   public int Id { get; set; }
   
   [Required]
   [DataMember(Name = "name")]
   public string Name { get; set; }
   
   [DataMember(Name = "displayName")]
   public string? DisplayName { get; set; }
   
   [DataMember(Name ="description")]
   public string? Description { get; set; }
   
   [DataMember(Name = "projects")]
   public List<Project>? Projects { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }
}