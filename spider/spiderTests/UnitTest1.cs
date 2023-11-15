using spider.Services;


namespace spiderTests;

[TestFixture]
public class Tests
{
    private GitHubGraphqlService _spiderGithubGraphqlService;
    [SetUp]
    public void Setup()
    {

        //_spiderGithubGraphqlService = new GitHubGraphqlService();
    }

    [Test]
    public async Task SearchResultTest()
    {
        var result = await _spiderGithubGraphqlService.QueryRepositoriesByName("API_Test_Repo");
        Assert.IsNotNull(result.Search);
    }
}