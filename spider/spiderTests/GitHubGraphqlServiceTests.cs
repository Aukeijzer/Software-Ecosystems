// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

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

    [Test]
    public async Task GetRepoCount()
    {
        var client = new Mock<IClientWrapper>();
        client.Setup<Task<GraphQLResponse<SearchCountData>>>(x => x.SendQueryAsync<SearchCountData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SearchCountData>(
            new GraphQLResponse<SearchCountData>()
            {
                Data = new SearchCountData()
                {
                    Search = new RepositoryCountData()
                    {
                        RepositoryCount = 100
                    }
                }
            }, null!,
            HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        var result = await _gitHubGraphqlService.GetRepoCount("test", 5, 10);
        Assert.AreEqual(100, result);
    }

    [Test]
    public async Task GetRepoCountErrorTest()
    {
        //Test where the response has errors
        var client = new Mock<IClientWrapper>();
        client.Setup<Task<GraphQLResponse<SearchCountData>>>(x => x.SendQueryAsync<SearchCountData>(
            It.IsAny<GraphQLHttpRequest>())).ReturnsAsync(new GraphQLHttpResponse<SearchCountData>(
            new GraphQLResponse<SearchCountData>()
            {
                Data = new SearchCountData()
                {
                    Search = new RepositoryCountData()
                    {
                        RepositoryCount = 100
                    }
                },
                Errors = _errors.ToArray()
            }, null!,
            HttpStatusCode.Accepted));
        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        var result = await _gitHubGraphqlService.GetRepoCount("test", 5, 10);
        Assert.AreEqual(100, result);
        
        //Test where the client throws a bad gateway exception
        client.Setup<Task<GraphQLResponse<SearchCountData>>>(x => x.SendQueryAsync<SearchCountData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.BadGateway, null!, "Bad Gateway"));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);
        
        Assert.ThrowsAsync<BadHttpRequestException>(async () =>
            await _gitHubGraphqlService.GetRepoCount("test", 5, 10));
        client.Verify(x => x.SendQueryAsync<SearchCountData>(It.IsAny<GraphQLHttpRequest>()),
            Times.Exactly(4));

        //Test where the client throws a not found exception
        client.Setup<Task<GraphQLResponse<SearchCountData>>>(x => x.SendQueryAsync<SearchCountData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new GraphQLHttpRequestException(
            HttpStatusCode.NotFound, null!, "Not Found"));

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<GraphQLHttpRequestException>(async () =>
            await _gitHubGraphqlService.GetRepoCount("test", 5, 10));

        //Test where the client throws a null reference exception
        client.Setup<Task<GraphQLResponse<SearchCountData>>>(x => x.SendQueryAsync<SearchCountData>(
            It.IsAny<GraphQLHttpRequest>())).Throws(new NullReferenceException());

        _gitHubGraphqlService = new GitHubGraphqlService(client.Object);

        Assert.ThrowsAsync<NullReferenceException>(async () =>
            await _gitHubGraphqlService.GetRepoCount("test", 5, 10));
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
        for (int i = 0; i < 30; i++)
        {
            test.Add(new ProjectRequestDto {RepoName = "agriculture", OwnerName = "seco"});
        }
        await _gitHubGraphqlService.GetByNames(test);
        client.Verify(x => x.SendQueryAsync<SpiderData>(It.Is<GraphQLHttpRequest>(
            y => y.Variables.ToString().Contains("repo:seco/agriculture"))), Times.Exactly(3));
    }
}