using System.Runtime.Serialization;
using SECODashBackend.Dtos.ProgrammingLanguage;

namespace SECODashBackend.Dtos.Project;

/// <summary>
/// Represents a data transfer object for a Project.
/// </summary>
[DataContract]
public class ProjectDto
{
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   
   [DataMember(Name = "createdAt")]
   public DateTime CreatedAt { get; set; }
   
   [DataMember(Name = "latestDefaultBranchCommitDate")]
   public DateTime? LatestDefaultBranchCommitDate { get; set; }
   
   [DataMember(Name = "owner")]
   public required string Owner { get; set; }
   
   [DataMember(Name = "description")]
   public string? Description { get; set; }

   [DataMember(Name = "topics")] 
   public List<string> Topics { get; set; } = new();

   [DataMember(Name = "languages")]
   public List<ProgrammingLanguageDto> Languages { get; set; } = new();
   
   [DataMember(Name = "totalSize")]
   public int? TotalSize { get; set; }

   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int NumberOfStars { get; set; }
   
   [DataMember(Name = "contributors")]
   public List<ContributorDto>? Contributors { get; set; }
}
