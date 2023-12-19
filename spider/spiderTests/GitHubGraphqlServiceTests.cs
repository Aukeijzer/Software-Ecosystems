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
    private GitHubGraphqlService _gitHubGraphqlService = null!;
    private List<GraphQLError> _errors =
    [
        new GraphQLError()
        {
            Message = "An error occurred"
        }

    ];

    /// <summary>
    /// This tests the QueryRepositoriesByNameHelper method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task QueryRepositoriesByNameHelperTest()
    {
        var client = new Mock<IClientWrapper>();
        //Test where hasNextPage is true
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
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
            }, null!,
            HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where hasNextPage is false
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
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
            }, null!,
            HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 50, "test");
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
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
        var client = new Mock<IClientWrapper>();
        //Test where the response has errors
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
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
            null!, HttpStatusCode.Accepted));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 49, "test");
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where the client throws a bad gateway exception
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null!, "Bad Gateway"));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(5));

        //Test where the client throws a not found exception
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.NotFound, null!, "Not Found"));
        
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        
        Assert.ThrowsAsync<GraphQLHttpRequestException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));

        //Test where the client throws a null reference exception
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new NullReferenceException());

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByNameHelper("test", 25, "test"));
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
        var client = new Mock<IClientWrapper>();
        //Test where hasNextPage is true
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
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
            }, null!, HttpStatusCode.Accepted));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 49, null);
        client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where hasNextPage is false
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
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
            }, null!, HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 50, "test");
        client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
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
        var client = new Mock<IClientWrapper>();
        //Test where the response has errors
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
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
            }, null!,
            HttpStatusCode.Accepted));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 49, null);
        client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(2));

        //Test where the client throws a bad gateway exception
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null!, "Bad Gateway"));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));
        client.Verify(x => x.SendQueryAsync<TopicSearchData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(5));

        //Test where the client throws a not found exception
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.NotFound, null!, "Not Found"));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<GraphQLHttpRequestException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));

        //Test where the client throws a null reference exception
        client.Setup<Task<GraphQLResponse<TopicSearchData>>>(x => x.SendQueryAsync<TopicSearchData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new NullReferenceException());

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _gitHubGraphqlService.QueryRepositoriesByTopicHelper("test", 25, "test"));
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
        var client = new Mock<IClientWrapper>();
        client.Setup<Task<GraphQLResponse<RepositoryWrapper>>>(x => x.SendQueryAsync<RepositoryWrapper>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<RepositoryWrapper>(
            new GraphQLResponse<RepositoryWrapper>()
            {
                Data = new RepositoryWrapper()
            }, null!,
            HttpStatusCode.Accepted));
        
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        await _gitHubGraphqlService.QueryRepositoryByName("test", "test");
        client.Verify(x => x.SendQueryAsync<RepositoryWrapper>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(1));
    }
    
    /// <summary>
    /// This tests the ToQueryString method of the GitHubGraphqlService.
    /// It tests if the method calls the SendQueryAsync method of the client.
    /// We test this by mocking the client and checking if the SendQueryAsync method is called the correct number of
    /// times.
    /// </summary>
    [Test]
    public async Task GetByNamesTests()
    {
        var client = new Mock<IClientWrapper>();
        client.Setup<Task<GraphQLResponse<SpiderData>>>(x => x.SendQueryAsync<SpiderData>(
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
            }, null!,
            HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        
        var test = new List<ProjectRequestDto> { new() {RepoName = "agriculture", OwnerName = "seco"} };
        await _gitHubGraphqlService.GetByNames(test);
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.Is<GraphQLHttpRequest>(
                y => y.Variables.ToString().Contains("repo:seco/agriculture"))), Times.Once);
    }
}