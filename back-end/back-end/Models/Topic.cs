using System.Runtime.Serialization;

namespace SECODashBackend.Models;
[DataContract]
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