using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace spider.Dtos;

[DataContract]
public class ProjectRequestDto
{
    [Required] 
    [DataMember(Name = "ownerNames")] 
    public required string ownerName { get; init; }
    
    [DataMember(Name = "repoName")] 
    public required string repoName { get; init; }
}