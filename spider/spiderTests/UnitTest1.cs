using spider.Services;


namespace spiderTests;

[TestFixture]
public class Tests
{
    private GitHubService spiderGithubService;
    [SetUp]
    public void Setup()
    {

        spiderGithubService = new GitHubService();
    }

    [Test]
    public async Task SearchResultTest()
    {
        var result = await spiderGithubService.QueryRepositoriesByName("API_Test_Repo");
        Assert.IsNotNull(result.Search);
    }
}