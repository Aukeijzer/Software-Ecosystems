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

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace SECODashBackend.Models;

/// <summary>
/// A banned topic is a topic the creator of an ecosystem has flagged to not be shown on the dashboard.
/// </summary>
public class BannedTopic
{
    /// <summary>
    /// The term used in taxonomy for software ecosystems.
    /// </summary>
    [DataMember(Name = "term")] 
    [Key]
    public required string Term { get; set; }
    /// <summary>
    /// The ecosystems in which this is term is a banned topic.
    /// </summary>
    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = [];
}