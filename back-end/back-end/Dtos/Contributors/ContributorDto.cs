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

namespace SECODashBackend.Dtos.Contributors;

/// <summary>
/// Represents a data transfer object for a Contributor of a Project.
/// </summary>
[DataContract]
public class ContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "id")]
    public required int Id { get; init; }
    [DataMember(Name = "nodeId")]
    public string? NodeId { get; init; }
    [DataMember(Name = "contributions")]
    public int? Contributions { get; init; }
    [DataMember(Name = "type")]
    public string? Type { get; init; }
}