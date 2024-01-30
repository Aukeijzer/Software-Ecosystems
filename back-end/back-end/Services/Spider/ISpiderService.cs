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

using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.Spider;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects.
/// </summary>
public interface ISpiderService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic.
    /// </summary>
    /// <param name="topic">The topic to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount);
    /// <summary>
    /// Requests the Spider for projects related to the given keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount);
}