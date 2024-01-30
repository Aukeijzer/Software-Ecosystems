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

namespace SECODashBackend.Dtos.Ecosystem;

/// <summary>
/// Represents a data transfer object for the supplementary data of a top level Ecosystem
/// such as Agriculture, Quantum or Artificial Intelligence.
/// </summary>
public class EcosystemOverviewDto
{
    /// <summary>
    /// The name of the ecosystem.
    /// </summary>
    [DataMember(Name = "name")] public required string Name { get; init; }
    
    /// <summary>
    /// The display name of the ecosystem.
    /// </summary>
    [DataMember(Name = "displayName")] public string? DisplayName { get; set; }
    
    /// <summary>
    /// the description of the ecosystem.
    /// </summary>
    [DataMember(Name = "description")] public string? Description { get; set; }
    
    /// <summary>
    /// The total number of projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfProjects")] public long? NumberOfProjects { get; set; }
    
    /// <summary>
    /// The total number of subtopics in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfSubTopics")] public long? NumberOfSubTopics { get; set; }
    
    /// <summary>
    /// The total number of contributors to all projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfContributors")] public long? NumberOfContributors { get; set; }
    
    /// <summary>
    /// The total number of stars of all projects in the ecosystem.
    /// </summary>
    [DataMember(Name = "numberOfStars")] public long? NumberOfStars { get; set; }
}