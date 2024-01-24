using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;

/// <summary>
/// This class represents an Ecosystem.
/// An Ecosystem is a collection of projects that are related to each other.
/// </summary>
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
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }

   [DataMember(Name = "users")]
   public List<User> Users { get; set; } = [];

   [DataMember(Name = "taxonomy")] 
   public List<Taxonomy> Taxonomy { get; set; } = [];

   [DataMember(Name = "technologies")] 
   public List<Technology> Technologies { get; set; } = [];
}