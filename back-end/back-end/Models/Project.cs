using System.Runtime.Serialization;

namespace SECODashBackend.Models;

public class Project
{
    [DataMember(Name = "id")]
    public required string Id { get; init; }
   
    [DataMember(Name = "name")]
    public required string Name { get; set; }
    
    [DataMember(Name = "createdAt")]
    public DateTime CreatedAt { get; init; }

    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = new();
   
    [DataMember(Name = "owner")]
    public required string Owner { get; set; }
   
    [DataMember(Name = "description")]
    public string? Description { get; set; }

    [DataMember(Name = "topics")] public List<string> Topics { get; set; } = new();

    [DataMember(Name = "languages")] public List<ProjectProgrammingLanguage> Languages { get; set; } = new();
   
    [DataMember(Name = "totalSize")]
    public int? TotalSize { get; set; }

    [DataMember(Name = "readme")]
    public string? ReadMe { get; set; }
   
    [DataMember(Name = "numberOfStars")]
    public int NumberOfStars { get; set; }
}