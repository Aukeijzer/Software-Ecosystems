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

namespace SECODashBackend.Services.Projects;

/// <summary>
/// Interface for a service that is responsible for requesting the Spider for projects and saving them to the database.
/// </summary>
public interface IProjectsService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic and saves them to the database.
    /// </summary>
    /// <param name="topic">The topic to to search for. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task MineByTopicAsync(string topic, string ecosystem, int amount);
    /// <summary>
    /// Requests the Spider for projects related to the given keyword and saves them to the database.
    /// </summary>
    /// <param name="keyword">The keyword to to search for. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The amount of repos to search for. </param>
    public Task MineByKeywordAsync(string keyword, string ecosystem, int amount);
    /// <summary>
    /// Requests the Spider for projects related to the given taxonomy and saves them to the database.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    public Task MineByTaxonomyAsync(List<string> taxonomy, string ecosystem, int keywordAmount, int topicAmount);
}