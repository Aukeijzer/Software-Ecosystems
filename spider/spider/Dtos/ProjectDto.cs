// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.Serialization;

namespace spider.Dtos;

/// <summary>
/// A data transfer object for a single repository
/// </summary>
[DataContract]
public class ProjectDto
{
   [DataMember(Name = "id")]
   public required string Id { get; set; }
   
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   
   [DataMember(Name = "createdAt")]
   public DateTime CreatedAt { get; set; }
   
   [DataMember(Name = "pushedAt")]
   public DateTime? PushedAt { get; set; }
   
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
   
   [DataMember(Name = "contributors")]
   public List<ContributorDto>? Contributors { get; set; }
}