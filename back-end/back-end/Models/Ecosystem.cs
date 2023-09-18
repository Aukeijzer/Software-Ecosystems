using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace SECODashBackend.Models;

[DataContract]
public class Ecosystem
{
   public Ecosystem(int id, string name, string displayName, List<Project>? projects,   int? numberOfStars)
   {
      Id = id;
      Name = name;
      DisplayName = displayName;
      Projects = projects;
      NumberOfStars = numberOfStars;
   }
   [Required]
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