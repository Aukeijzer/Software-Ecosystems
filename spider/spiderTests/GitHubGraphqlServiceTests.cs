using System.Net;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Http;
using Moq;
using spider.Services;
using spider.Wrappers;

namespace spiderTests;

[TestFixture]
public class GitHubGraphqlServiceTests
{
    private GitHubGraphqlService _GitHubGraphqlService;
    private Mock<IClientWrapper> _client;
    private List<GraphQLError> _errors;

    [SetUp]
    public void setup()
    {
        _client = new Mock<IClientWrapper>();
        _errors = new List<GraphQLError>();
        _errors.Add(new GraphQLError()
        {
            Message = "An error occurred"
        });
    }

    [Test]
    public async Task QueryRepositoriesByNameHelperTest()
    {
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
                It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>() {Data = new SpiderData() {Search = new SearchResult() {Nodes = [] ,
                PageInfo = new PageInfo() {EndCursor = "cursor", HasNextPage = true}}}}, null,
            HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));
        
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>() {Data = new SpiderData() {Search = new SearchResult() {Nodes = [] ,
                PageInfo = new PageInfo() {EndCursor = "cursor", HasNextPage = false}}}}, null,
            HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 50, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(3));
    }

    [Test]
    public async Task QueryRepositoriesByNameErrorTests()
    {
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>() {Data = new SpiderData() {Search = new SearchResult() {Nodes = [] ,
                PageInfo = new PageInfo() {EndCursor = "cursor", HasNextPage = true}}}, Errors = _errors.ToArray()},
            null, HttpStatusCode.Accepted));
        
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));
        
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null, "Bad Gateway"));
        
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        
        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));
         _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
             Times.Exactly(5));
    }
}