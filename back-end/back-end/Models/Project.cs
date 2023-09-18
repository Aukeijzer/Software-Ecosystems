using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SECODashBackend.Models;

[DataContract]
public class Project // : IEquatable<Project>
{
   public Project(string name, long? id, string? about, string? owner, string? readMe, int? numberOfStars)
   {
      Name = name;
      Id = id;
      Owner = owner;
      ReadMe = readMe;
      NumberOfStars = numberOfStars;
   }
   [DataMember(Name = "id")]
   public long? Id { get; set; }
   
   [Required]
   [DataMember(Name = "name")]
   public string Name { get; set; }
   [DataMember(Name = "about")]
   public string? About { get; set; }
   
   [DataMember(Name = "owner")]
   public string? Owner { get; set; }
   

   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }
}