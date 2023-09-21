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
    private GitHubService sgs;
    [SetUp]
    public void Setup()
    {

        sgs = new GitHubService();
    }

    [Test]
    public async Task searchResultTest()
    {
        var result = sgs.QueryRepositoriesByName("API_Test_Repo");
        Assert.IsNotNull(result.Result);
    }
}