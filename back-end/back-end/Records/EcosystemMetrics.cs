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

namespace SECODashBackend.Records;

/// <summary>
/// The metrics of a top-level ecosystem.
/// </summary>
public record EcosystemMetrics
{
    /// <summary>
    /// The total number of projects in the ecosystem.
    /// </summary>
    public long NumberOfProjects { get; init; }
    
    /// <summary>
    /// The total number of subtopics in the ecosystem.
    /// </summary>
    public long NumberOfSubTopics { get; init; }
    
    /// <summary>
    /// The total number of contributors to all projects in the ecosystem.
    /// </summary>
    public long NumberOfContributors { get; init; }
    
    /// <summary>
    /// The total number of stars of all projects in the ecosystem.
    /// </summary>
    public long NumberOfStars { get; init; }
}