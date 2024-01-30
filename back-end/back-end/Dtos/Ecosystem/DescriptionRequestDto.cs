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

namespace SECODashBackend.Dtos.Ecosystem;
using System.Runtime.Serialization;

/// <summary>
/// Represents a request DTO for updating the description of an ecosystem.
/// </summary>
public class DescriptionRequestDto
{
    [DataMember(Name = "description")]
    public required string Description { get; init; }
    
    [DataMember(Name = "ecosystem")]
    public required string Ecosystem { get; init; }
}