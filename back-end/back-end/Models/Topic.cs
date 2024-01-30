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

namespace SECODashBackend.Models;

/// <summary>
/// This class represents a Topic.
/// A Topic is a keyword that is related to a project and is used to define ecosystems.
/// </summary>
public class Topic
{
    [DataMember(Name ="id")]
    public int? Id { get; set; }
    
    [DataMember(Name = "label")]
    public string? Label { get; set; }
    
    [DataMember(Name = "keywords")]
    public required List<string> Keywords { get; init; }
    
    [DataMember(Name = "probability")]
    public required float Probability { get; init; }
}