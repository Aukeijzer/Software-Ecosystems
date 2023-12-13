using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Project;

/// <summary>
/// Represents a data transfer object for a Project requested from the Spider.
/// </summary>
[DataContract]
public class ProjectRequestDto
{
    [DataMember(Name = "ownerName")] 
    public required string OwnerName { get; init; }
    
    [DataMember(Name = "repoName")] 
    public required string RepoName { get; init; }
}