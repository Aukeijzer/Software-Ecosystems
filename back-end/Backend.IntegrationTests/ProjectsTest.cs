using System.Net;
using FluentAssertions;
using RestSharp;

namespace Backend.IntegrationTests;

public class ProjectsTest
{
    /// <summary>
    /// This method tests the Post method of the ProjectsController that mines projects based on topics.
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