using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Spider;

namespace BackendTests;
[TestFixture]
public class SpiderServiceUnitTests
{
    private readonly SpiderService testSpiderService = new SpiderService("http://localhost:5205/Spider");
    
    /// <summary>
    /// This tests the GetProjectsByTopicAsync method of the SpiderService.
    /// </summary>
    [Test]
    public void GetProjectsByTopicAsync_ReturnsProjects()
    {
        // Arrange
        string testTopic = "ai";
        int amount = 10;
        
        // Act
        Task<List<ProjectDto>> result = testSpiderService.GetProjectsByTopicAsync(testTopic, amount);
        
        // Assert
        var returnList = result.Result;
        Assert.That(result.Exception, Is.Null);
        Assert.That(returnList, Is.Not.Null);
        Assert.That(returnList.Count, Is.EqualTo(amount));
        Assert.IsNotEmpty(returnList.FindAll(p => p.Topics.Contains(testTopic)));
    }
    
    /// <summary>
    /// This tests the GetProjectsByKeywordAsync method of the SpiderService.
    /// </summary>
    [Test]
    public void GetProjectsByKeywordAsync_ReturnsProjects()
    {
        // Arrange
        string testKeyword = "quantum";
        int amount = 10;
        
        // Act
        Task<List<ProjectDto>> result = testSpiderService.GetProjectsByKeywordAsync(testKeyword, amount);
        
        // Assert
        var returnList = result.Result;
        Assert.That(result.Exception, Is.Null);
        Assert.That(returnList, Is.Not.Null);
        Assert.That(returnList.Count, Is.EqualTo(amount));
        Assert.IsNotEmpty(returnList.FindAll(p => p.Name.Contains(testKeyword)));
    }
}