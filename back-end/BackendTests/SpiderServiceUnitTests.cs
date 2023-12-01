using SECODashBackend.Dtos.Project;

namespace BackendTests;
[TestFixture]
public class SpiderServiceUnitTests
{
    private readonly TSpiderService testSpiderService = new TSpiderService();
    
    [Test]
    public void GetProjectsByTopicAsync_ReturnsEmptyList()
    {
        // Arrange
        string testTopic = "test";
        int amount = 1;
        
        // Act
        Task<List<ProjectDto>> result = testSpiderService.GetProjectsByTopicAsync(testTopic, amount);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        
    }
}