using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SECODashBackend.Models;

[DataContract]
public class Project
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
   
    [DataMember(Name = "Topics")]
    public string[]? Topics { get; set; }
   
    [DataMember(Name = "Languages")]
    public string[]? Languages { get; set; }
   
    [DataMember(Name = "LanguageSizes")]
    public int[]? LanguageSizes { get; set; }
   
    [DataMember(Name = "TotalSize")]
    public int? TotalSize { get; set; }

    [DataMember(Name = "readme")]
    public string? ReadMe { get; set; }
   
    [DataMember(Name = "numberOfStars")]
    public int? NumberOfStars { get; set; }
}