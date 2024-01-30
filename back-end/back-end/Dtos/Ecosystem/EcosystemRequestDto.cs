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
/// Represents a data transfer object for an Ecosystem request by the front-end.
/// </summary>
public class EcosystemRequestDto
{
    [DataMember(Name = "topics")] public required List<string> Topics { get; init; }
    [DataMember(Name = "numberOfTopLanguages")] public int? NumberOfTopLanguages { get; set; }
    [DataMember(Name = "numberOfSubEcosystems")] public int? NumberOfTopSubEcosystems { get; set; }
    [DataMember(Name = "numberOfTopContributors")] public int? NumberOfTopContributors { get; set; }
    [DataMember(Name = "numberOfTopTechnologies")] public int? NumberOfTopTechnologies { get; set; }
    [DataMember(Name = "numberOfTopProjects")] public int? NumberOfTopProjects { get; set; }
    [DataMember(Name = "startTime")] public DateTime StartTime { get; set; }
    [DataMember(Name = "endTime")] public DateTime EndTime { get; set; }
    [DataMember(Name = "numbersOfDaysPerBucket")] public int? NumbersOfDaysPerBucket { get; set; }
}