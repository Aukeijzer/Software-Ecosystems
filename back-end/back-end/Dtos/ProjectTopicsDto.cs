using System.Runtime.Serialization;
using SECODashBackend.Models;

namespace SECODashBackend.Dtos;

/// <summary>
/// Data Transfer Object for the Topics related to a Project
/// </summary>
[DataContract]
public class ProjectTopicsDto
{
    // public required long ProjectId { get; set; }
    public long? ProjectId { get; set; }
    
    [DataMember(Name = "topics")]
    public required List<Topic> Topics { get; set; }
}