using System.Runtime.Serialization;

namespace SECODashBackend.Models;

/// <summary>
/// This class represents a Topic.
/// A Topic is a keyword that is related to a project and is used to define ecosystems.
/// </summary>
public class Topic
{
    [DataMember(Name ="id")]
    public int? Id { get; set; }
    
    [DataMember(Name = "label")]
    public string? Label { get; set; }
    
    [DataMember(Name = "keywords")]
    public required List<string> Keywords { get; init; }
    
    [DataMember(Name = "probability")]
    public required float Probability { get; init; }
}