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

using System.Net;
using FluentAssertions;
using RestSharp;

namespace Backend.IntegrationTests;

public class ProjectsTest
{
    /// <summary>
    /// This method tests the Post method of the ProjectsController that mines projects based on topics and keywords.
    /// </summary>
    [Fact]
    public async Task Post_Projects_ReturnTopicProjects()
    {
        // Arrange
        string testtopic = "quantum";
        string testkeyword = "computer";
        int amount = 10;
        var restClient = new RestClient("http://localhost:5205/Spider");
        var requestTopic = new RestRequest("topic/" + testtopic + "/" + amount);
        var requestKeyword = new RestRequest("name/" + testkeyword + "/" + amount);
        
        // Act
        // Let the RestSharp client execute the request and await the response.
        var responseTopic = await restClient.ExecuteAsync(requestTopic);
        var responseKeyword = await restClient.ExecuteAsync(requestKeyword);
        
        // Assert
        // Check if the response is OK.
        responseTopic.StatusCode.Should().Be(HttpStatusCode.OK);
        responseKeyword.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}