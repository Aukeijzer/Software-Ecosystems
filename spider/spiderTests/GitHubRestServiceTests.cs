using System.Net;
using Moq;
using RestSharp;
using spider.Dtos;
using spider.Services;
using spider.Wrappers;

namespace spiderTests;

[TestFixture]
public class GitHubRestServiceTests
{
    private GitHubRestService _gitHubRestService;
    private Mock<IRestClient> _restClient;
    private string _restResult, _restResultFifty;

    [SetUp]
    public void setup()
    {
        _restClient = new Mock<IRestClient>();
        _restResultFifty = "[\n  {\n    \"login\": \"allcontributors[bot]\",\n    \"id\": 46447321,\n    \"nodeId\": null,\n    \"contributions\": 88,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"jakebolam\",\n    \"id\": 3534236,\n    \"nodeId\": null,\n    \"contributions\": 67,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"dependabot[bot]\",\n    \"id\": 49699333,\n    \"nodeId\": null,\n    \"contributions\": 30,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"Berkmann18\",\n    \"id\": 8260834,\n    \"nodeId\": null,\n    \"contributions\": 19,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"tenshiAMD\",\n    \"id\": 13580338,\n    \"nodeId\": null,\n    \"contributions\": 10,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"robertlluberes\",\n    \"id\": 13991439,\n    \"nodeId\": null,\n    \"contributions\": 6,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"chrissimpkins\",\n    \"id\": 4249591,\n    \"nodeId\": null,\n    \"contributions\": 4,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Jameskmonger\",\n    \"id\": 2037007,\n    \"nodeId\": null,\n    \"contributions\": 4,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"greenkeeper[bot]\",\n    \"id\": 23040076,\n    \"nodeId\": null,\n    \"contributions\": 4,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"ben-eb\",\n    \"id\": 1282980,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"bogas04\",\n    \"id\": 6177621,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jamesplease\",\n    \"id\": 2322305,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"mbad0la\",\n    \"id\": 8503331,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Roshanjossey\",\n    \"id\": 8488446,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"MM-coder\",\n    \"id\": 22800592,\n    \"nodeId\": null,\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"CassVenere\",\n    \"id\": 47280556,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"ilai-deutel\",\n    \"id\": 10098207,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"itaisteinherz\",\n    \"id\": 22768990,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"sinchang\",\n    \"id\": 3297859,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jfmengels\",\n    \"id\": 3869412,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jsoref\",\n    \"id\": 2119212,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Frijol\",\n    \"id\": 454690,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"mpeyper\",\n    \"id\": 23029903,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"patcon\",\n    \"id\": 305339,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"sirpeas\",\n    \"id\": 4818642,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"codimiracle\",\n    \"id\": 21952540,\n    \"nodeId\": null,\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"0xflotus\",\n    \"id\": 26602940,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"abe-101\",\n    \"id\": 82916197,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"abinoda\",\n    \"id\": 50083,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"atuttle\",\n    \"id\": 46990,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"z0al\",\n    \"id\": 12673605,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alanyee\",\n    \"id\": 1873994,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alejandronanez\",\n    \"id\": 464978,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alexwlchan\",\n    \"id\": 301220,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"ali-master\",\n    \"id\": 9049092,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"allanbowe\",\n    \"id\": 4420615,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"andrewmcodes\",\n    \"id\": 18423853,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"austinhuang0131\",\n    \"id\": 16656689,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"vedantmgoyal2009\",\n    \"id\": 83997633,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"corneliusroemer\",\n    \"id\": 25161793,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"The24thDS\",\n    \"id\": 26633429,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"DemianD\",\n    \"id\": 5346497,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"e-coders\",\n    \"id\": 83082760,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"EndBug\",\n    \"id\": 26386270,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"King-BR\",\n    \"id\": 51011050,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"fhemberger\",\n    \"id\": 153481,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"guylepage3\",\n    \"id\": 1711854,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Haroenv\",\n    \"id\": 6270048,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"diegohaz\",\n    \"id\": 3068563,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"nhnb\",\n    \"id\": 364184,\n    \"nodeId\": null,\n    \"contributions\": 1,\n    \"type\": \"User\"\n  }\n]";
        _restResult = "[\n  {\n    \"login\": \"burisu\",\n    \"id\": 240595,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/240595?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/burisu\",\n    \"html_url\": \"https://github.com/burisu\",\n    \"followers_url\": \"https://api.github.com/users/burisu/followers\",\n    \"following_url\": \"https://api.github.com/users/burisu/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/burisu/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/burisu/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/burisu/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/burisu/orgs\",\n    \"repos_url\": \"https://api.github.com/users/burisu/repos\",\n    \"events_url\": \"https://api.github.com/users/burisu/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/burisu/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 3447\n  },\n  {\n    \"login\": \"ionosphere\",\n    \"id\": 1838491,\n    \"node_id\": \"U_kgDOABwNmw\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/1838491?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/ionosphere\",\n    \"html_url\": \"https://github.com/ionosphere\",\n    \"followers_url\": \"https://api.github.com/users/ionosphere/followers\",\n    \"following_url\": \"https://api.github.com/users/ionosphere/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/ionosphere/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/ionosphere/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/ionosphere/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/ionosphere/orgs\",\n    \"repos_url\": \"https://api.github.com/users/ionosphere/repos\",\n    \"events_url\": \"https://api.github.com/users/ionosphere/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/ionosphere/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 3348\n  },\n  {\n    \"login\": \"Aquaj\",\n    \"id\": 2871879,\n    \"node_id\": \"U_kgDOACvSRw\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/2871879?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/Aquaj\",\n    \"html_url\": \"https://github.com/Aquaj\",\n    \"followers_url\": \"https://api.github.com/users/Aquaj/followers\",\n    \"following_url\": \"https://api.github.com/users/Aquaj/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/Aquaj/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/Aquaj/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/Aquaj/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/Aquaj/orgs\",\n    \"repos_url\": \"https://api.github.com/users/Aquaj/repos\",\n    \"events_url\": \"https://api.github.com/users/Aquaj/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/Aquaj/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 2022\n  }]";
    }

    /// <summary>
    /// Tests the GetRepoContributors method of the GitHubRestService.
    /// Checks that the method returns the correct number of contributors and that it makes the correct number of
    /// requests to the GitHub API.
    /// We test this by mocking the client and setting up the response to return a list of contributors.
    /// This test includes a lot of similar tests with slightly different inputs to test the different paths in the
    /// GetRepoContributors method.
    /// </summary>
    [Test]
    public async Task GetRepoContributorsTests()
    {
        //Tests where the client returns 3 contributors
        _restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = _restResult,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        var result = await _gitHubRestService.GetRepoContributors("test", "test", 3);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(3, result.Count);
        
        result = await _gitHubRestService.GetRepoContributors("test", "test", 50);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(2));
        Assert.AreEqual(3, result.Count);
        
        result = await _gitHubRestService.GetRepoContributors("test", "test", 51);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(3));
        Assert.AreEqual(3, result.Count);
        
        //Tests where the client returns 50 contributors
        _restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = _restResultFifty,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        result = await _gitHubRestService.GetRepoContributors("test", "test", 3);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(4));
        Assert.AreEqual(3, result.Count);
        
        
        result = await _gitHubRestService.GetRepoContributors("test", "test", 100);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(6));
        Assert.AreEqual(100, result.Count);

        //Tests where the client returns 0 contributors
        _restClient.Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = "[]",
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed,
                ContentLength = 0
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        
        result = await _gitHubRestService.GetRepoContributors("test", "test", 100);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(7));
        Assert.AreEqual(new List<ContributorDto>(), result);
        
        result = await _gitHubRestService.GetRepoContributors("test", "test", 50);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(8));
        Assert.AreEqual(new List<ContributorDto>(), result);
    }
    
    /// <summary>
    /// Tests the GetRepoContributors method of the GitHubRestService.
    /// Checks that the method handles errors correctly.
    /// We test this by mocking the client and setting up the response to return an error.
    /// </summary>
    [Test]
    public async Task GetRepoContributorsErrorTests()
    {
        //Tests where the rate limit is exceeded
        List<HeaderParameter> headers = new List<HeaderParameter>();
        headers.Add(new HeaderParameter("X-RateLimit-Remaining", "0"));
        headers.Add(new HeaderParameter("X-RateLimit-Reset",
            (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 1).ToString()));
        
        _restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = _restResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        var result = await _gitHubRestService.GetRepoContributors("test", "test", 3);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        Assert.AreEqual(new List<ContributorDto>(), result);
        
        //Tests where the secondary rate limit is exceeded
        headers = new List<HeaderParameter>();
        headers.Add(new HeaderParameter("X-RateLimit-Remaining", "1"));
        headers.Add(new HeaderParameter("Retry-After", "1"));
        
        _restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = _restResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        result = await _gitHubRestService.GetRepoContributors("test", "test", 3);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(2));
        Assert.AreEqual(new List<ContributorDto>(), result);
        result = await _gitHubRestService.GetRepoContributors("test", "test", 51);
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(4));
        Assert.AreEqual(new List<ContributorDto>(), result);
        
        //Tests where the client returns an error
        headers = new List<HeaderParameter>();
        headers.Add(new HeaderParameter("X-RateLimit-Remaining", "1"));
        
        _restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = _restResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers,
                ErrorException = new NullReferenceException()
            });
        _gitHubRestService = new GitHubRestService(_restClient.Object);
        Assert.ThrowsAsync<NullReferenceException>(async () => await _gitHubRestService.GetRepoContributors("test", "test", 3));
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(5));
        Assert.ThrowsAsync<NullReferenceException>(async () => await _gitHubRestService.GetRepoContributors("test", "test", 51));
        _restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(6));
    }
}