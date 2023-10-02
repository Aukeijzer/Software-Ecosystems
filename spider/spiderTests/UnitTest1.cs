using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using spider.Services;
using spider.Controllers;
using spider.Converter;


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
    public async Task searchResultTest()
    {
        var result = spiderGithubService.QueryRepositoriesByName("API_Test_Repo", 10, "main:README.md");
        Assert.IsNotNull(result.Result);
    }
}