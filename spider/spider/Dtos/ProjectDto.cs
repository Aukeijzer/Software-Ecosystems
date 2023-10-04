﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace spider.Dtos;

[DataContract]
public class ProjectDto
{
   [Required]
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [Required]
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
   public string[]? Topics { get; set; }
   
   [DataMember(Name = "languages")]
   public ProgrammingLanguageDto[]? Languages { get; set; }
   
   [DataMember(Name = "totalSize")]
   public int? TotalSize { get; set; }

   [DataMember(Name = "readme")]
   public string? ReadMe { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int NumberOfStars { get; set; }
}