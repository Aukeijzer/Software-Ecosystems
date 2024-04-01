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

using spider.Dtos;

namespace spider.Services;

/// <summary>
/// ISpiderProjectService is the interface for the SpiderProjectService class. It contains the methods that are
/// implemented in SpiderProjectService
/// </summary>
public interface ISpiderProjectService
{
    public Task<List<ProjectDto>> GetByKeywordSplit(string name, int amount, string? startCursor);
    
    /// <summary>
    /// GetByKeyword takes a keyword, an amount and a start cursor and uses these to find the first amount of projects
    /// after the start cursor with the keyword as search phrase. The result includes contributors.
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns>A list of repositories including contributors in the form of List&lt;ProjectDto&gt;</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public Task<List<ProjectDto>> GetByKeyword(string name, int amount, string? startCursor);
    
    /// <summary>
    /// GetByTopic takes the name of a topic, an amount and a start cursor and uses those to get the first amount of
    /// repositories with the topic, after the start cursor. The result includes contributors.
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public Task<List<ProjectDto>> GetByTopic(string topic, int amount, string? startCursor);
    
    /// <summary>
    /// GetByName gets a repository based on it's name and ownerName
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">repository owner name</param>
    /// <returns>A single repository in the form of ProjectDto</returns>
    public Task<ProjectDto> GetByName(string name, string ownerName);
    
    /// <summary>
    /// GetByNames gets repositories by their name and ownerNames
    /// </summary>
    /// <param name="repos">A list of projectRequestDtos with at least a repoName and ownerName</param>
    /// <returns>Returns the list of requested repositories in the form of List&lt;ProjectDto&gt;</returns>
    public Task<List<ProjectDto>> GetByNames(List<ProjectRequestDto> repos);
    
    /// <summary>
    /// Get ContributorsByName gets the contributors of a repository based on the repositories name and ownerName
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">Repository owner name</param>
    /// <param name="amount">Amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?. Returns null if there are no
    /// contributors or they cannot be accessed</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public Task<List<ContributorDto>?> GetContributorsByName(string name, string ownerName, int amount);
}