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

using RestSharp;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.Spider;

/// <summary>
/// This service is responsible for requesting the Spider for projects.
/// </summary>
public class SpiderService : ISpiderService
{
    private readonly RestClient _spiderClient;

    public SpiderService(string connectionString)
    {
        var options = new RestClientOptions(connectionString);
        options.MaxTimeout = 100000000;
        _spiderClient = new RestClient(options);
    }
    
    /// <summary>
    /// Requests the Spider for projects related to the given keyword.
    /// </summary>
    /// <param name="keyword">The keyword to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task<List<ProjectDto>> GetProjectsByKeywordAsync(string keyword, int amount)
    {
        var request = new RestRequest("name/" + keyword + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }
    
    /// <summary>
    /// Requests the Spider for projects related to the given topic.
    /// </summary>
    /// <param name="topic">The topic to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task<List<ProjectDto>> GetProjectsByTopicAsync(string topic, int amount)
    {
        var request = new RestRequest("topic/" + topic + "/" + amount);
        // Throw an exception if the request cannot be deserialized into a List of Projects
        return await _spiderClient.GetAsync<List<ProjectDto>>(request) ?? throw new HttpRequestException();
    }
}