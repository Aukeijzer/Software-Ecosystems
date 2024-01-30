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

namespace SECODashBackend.Dtos.Topic;

/// <summary>
/// Data Transfer Object for a readme
/// </summary>
[DataContract]
public class TopicRequestDto
{
    [DataMember(Name = "id")]
    public required string Id { get; init; }
    
    [DataMember(Name = "name")]
    public required string Name { get; init; }
    
    [DataMember(Name = "description")] 
    public string? Description { get; set; }
    
    [DataMember(Name = "readMe")]
    public required string Readme { get; init; }
}