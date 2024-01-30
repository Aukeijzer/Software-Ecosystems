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
/// IGitHubRestService is an interface for the GitHub rest api.
/// </summary>
public interface IGitHubRestService
{
    /// <summary>
    /// GetRepoContributors sends a rest request to the github api and returns on success and otherwise handles the
    /// error and retries if necessary.
    /// </summary>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <param name="repoName">Name of the repository</param>
    /// <param name="amount">amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?</returns>
    public Task<List<ContributorDto>?> GetRepoContributors(String ownerName, string repoName, int amount = 50);
    
}