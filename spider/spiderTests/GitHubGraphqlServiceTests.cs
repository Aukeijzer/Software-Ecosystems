using System.Net;
using GraphQL;
using GraphQL.Client.Http;
using Microsoft.AspNetCore.Http;
using Moq;
using spider.Dtos;
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

    /// <summary>
    /// This tests the QueryRepositoriesByNameHelper method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task QueryRepositoriesByNameHelperTest()
    {
        //Test where hasNextPage is true
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>()
            {
                Data = new SpiderData()
                {
                    Search = new SearchResult()
                    {
                        Nodes = [],
                        PageInfo = new PageInfo() { EndCursor = "cursor", HasNextPage = true }
                    }
                }
            }, null,
            HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where hasNextPage is false
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>()
            {
                Data = new SpiderData()
                {
                    Search = new SearchResult()
                    {
                        Nodes = [],
                        PageInfo = new PageInfo() { EndCursor = "cursor", HasNextPage = false }
                    }
                }
            }, null,
            HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 50, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(3));
    }

    /// <summary>
    /// This tests the QueryRepositoriesByNameHelper method of the GitHubGraphqlService.
    /// It tests if the method throws the correct exception when the client throws an exception.
    /// We test this by mocking the client and checking if the method throws the correct exception when the client
    /// throws an exception.
    /// </summary>
    [Test]
    public async Task QueryRepositoriesByNameErrorTests()
    {
        //Test where the response has errors
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>()
            {
                Data = new SpiderData()
                {
                    Search = new SearchResult()
                    {
                        Nodes = [],
                        PageInfo = new PageInfo() { EndCursor = "cursor", HasNextPage = true }
                    }
                },
                Errors = _errors.ToArray()
            },
            null, HttpStatusCode.Accepted));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where the client throws a bad gateway exception
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null, "Bad Gateway"));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);

        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(5));

        //Test where the client throws a not found exception
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.NotFound, null, "Not Found"));
        
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        
        Assert.ThrowsAsync<GraphQLHttpRequestException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));

        //Test where the client throws a null reference exception
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new NullReferenceException());

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);

        Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));
    }

    /// <summary>
    /// This tests the QueryRepositoriesByTopicHelper method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task QueryRepositoriesByTopicHelperTest()
    {
        //Test where hasNextPage is true
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<TopicSearchData>(
            new GraphQLResponse<TopicSearchData>()
            {
                Data = new TopicSearchData()
                {
                    Topic = new TopicSearch()
                    {
                        Repositories = new TopicRepository()
                        {
                            Nodes = [], PageInfo = new PageInfo()
                                { EndCursor = "EndCursor", HasNextPage = true }
                        }
                    }
                }
            }, null, HttpStatusCode.Accepted));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 49, null);
        _client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where hasNextPage is false
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<TopicSearchData>(
            new GraphQLResponse<TopicSearchData>()
            {
                Data = new TopicSearchData()
                {
                    Topic = new TopicSearch()
                    {
                        Repositories = new TopicRepository()
                        {
                            Nodes = [], PageInfo = new PageInfo()
                            {
                                EndCursor = "EndCursor", HasNextPage = false
                            }
                        }
                    }
                }
            }, null, HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        result = await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 50, "test");
        _client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(3));
    }

    /// <summary>
    /// This tests the QueryRepositoriesByTopicHelper method of the GitHubGraphqlService.
    /// It tests if the method throws the correct exception when the client throws an exception.
    /// We test this by mocking the client and checking if the method throws the correct exception when the client
    /// throws an exception.
    /// </summary>
    [Test]
    public async Task QueryRepositoriesByTopicErrorTests()
    {
        //Test where the response has errors
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<TopicSearchData>(
            new GraphQLResponse<TopicSearchData>()
            {
                Data = new TopicSearchData()
                {
                    Topic = new TopicSearch()
                    {
                        Repositories = new TopicRepository()
                        {
                            Nodes = [], PageInfo = new PageInfo()
                                { EndCursor = "EndCursor", HasNextPage = true }
                        }
                    }
                },
                Errors = _errors.ToArray()
            }, null,
            HttpStatusCode.Accepted));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 49, null);
        _client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where the client throws a bad gateway exception
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null, "Bad Gateway"));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);

        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));
        _client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(5));

        //Test where the client throws a not found exception
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.NotFound, null, "Not Found"));

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);

        Assert.ThrowsAsync<GraphQLHttpRequestException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));

        //Test where the client throws a null reference exception
        _client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new NullReferenceException());

        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);

        Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _GitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));
    }

    /// <summary>
    /// This tests the QueryRepositoryByName method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task QueryRepositoryByNameTest()
    {
        _client.Setup<Task<GraphQLResponse<RepositoryWrapper>>>(x => x.SendQueryAsync<RepositoryWrapper>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<RepositoryWrapper>(
            new GraphQLResponse<RepositoryWrapper>()
            {
                Data = new RepositoryWrapper()
            }, null,
            HttpStatusCode.Accepted));
        
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        var result = await _GitHubGraphqlService.QueryRepositoryByName("test", "test");
        _client.Verify(x => x.SendQueryAsync<RepositoryWrapper>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(1));
    }
    
    /// <summary>
    /// This tests the ToQueryString method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task ToQueryStringTest()
    {
        _client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SpiderData>(
            new GraphQLResponse<SpiderData>()
            {
                Data = new SpiderData()
                {
                    Search = new SearchResult()
                    {
                        Nodes = [],
                        PageInfo = new PageInfo() { EndCursor = "cursor", HasNextPage = true }
                    }
                }
            }, null,
            HttpStatusCode.Accepted));
        _GitHubGraphqlService = new GitHubGraphqlService(_client.Object);
        
        var test = new List<ProjectRequestDto>();
        test.Add(new ProjectRequestDto() {RepoName = "test", OwnerName = "test"});
        var result = await _GitHubGraphqlService.ToQueryString(test);
        
        _client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Once);
    }
}