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
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;

/// <summary>
/// This class represents an Ecosystem.
/// An Ecosystem is a collection of projects that are related to each other.
/// </summary>
[Index(nameof(Name), IsUnique = true)]
public class Ecosystem
{  
   [DataMember(Name = "id")]
   public required string Id { get; init; }
   
   [DataMember(Name = "name")]
   public required string Name { get; set; }
   
   [DataMember(Name = "displayName")]
   public string? DisplayName { get; set; }
   
   [DataMember(Name ="description")]
   public string? Description { get; set; }
   
   [DataMember(Name = "numberOfStars")]
   public int? NumberOfStars { get; set; }

   [DataMember(Name = "users")]
   public List<User> Users { get; set; } = [];

   [DataMember(Name = "taxonomy")] 
   public List<Taxonomy> Taxonomy { get; set; } = [];

   [DataMember(Name = "technologies")] 
   public List<Technology> Technologies { get; set; } = [];

   [DataMember(Name = "bannedTopic")]
   public List<BannedTopic> BannedTopics { get; set; } = [];
}