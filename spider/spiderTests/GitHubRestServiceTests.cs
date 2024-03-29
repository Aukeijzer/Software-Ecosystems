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
using Moq;
using RestSharp;
using spider.Dtos;
using spider.Services;

namespace spiderTests;

[TestFixture]
public class GitHubRestServiceTests
{
    private const string  RestResult = "[\n  {\n    \"login\": \"burisu\",\n    \"id\": 240595,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/240595?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/burisu\",\n    \"html_url\": \"https://github.com/burisu\",\n    \"followers_url\": \"https://api.github.com/users/burisu/followers\",\n    \"following_url\": \"https://api.github.com/users/burisu/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/burisu/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/burisu/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/burisu/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/burisu/orgs\",\n    \"repos_url\": \"https://api.github.com/users/burisu/repos\",\n    \"events_url\": \"https://api.github.com/users/burisu/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/burisu/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 3447\n  },\n  {\n    \"login\": \"ionosphere\",\n    \"id\": 1838491,\n    \"node_id\": \"U_kgDOABwNmw\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/1838491?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/ionosphere\",\n    \"html_url\": \"https://github.com/ionosphere\",\n    \"followers_url\": \"https://api.github.com/users/ionosphere/followers\",\n    \"following_url\": \"https://api.github.com/users/ionosphere/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/ionosphere/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/ionosphere/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/ionosphere/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/ionosphere/orgs\",\n    \"repos_url\": \"https://api.github.com/users/ionosphere/repos\",\n    \"events_url\": \"https://api.github.com/users/ionosphere/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/ionosphere/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 3348\n  },\n  {\n    \"login\": \"Aquaj\",\n    \"id\": 2871879,\n    \"node_id\": \"U_kgDOACvSRw\",\n    \"avatar_url\": \"https://avatars.githubusercontent.com/u/2871879?v\\u003d4\",\n    \"gravatar_id\": \"\",\n    \"url\": \"https://api.github.com/users/Aquaj\",\n    \"html_url\": \"https://github.com/Aquaj\",\n    \"followers_url\": \"https://api.github.com/users/Aquaj/followers\",\n    \"following_url\": \"https://api.github.com/users/Aquaj/following{/other_user}\",\n    \"gists_url\": \"https://api.github.com/users/Aquaj/gists{/gist_id}\",\n    \"starred_url\": \"https://api.github.com/users/Aquaj/starred{/owner}{/repo}\",\n    \"subscriptions_url\": \"https://api.github.com/users/Aquaj/subscriptions\",\n    \"organizations_url\": \"https://api.github.com/users/Aquaj/orgs\",\n    \"repos_url\": \"https://api.github.com/users/Aquaj/repos\",\n    \"events_url\": \"https://api.github.com/users/Aquaj/events{/privacy}\",\n    \"received_events_url\": \"https://api.github.com/users/Aquaj/received_events\",\n    \"type\": \"User\",\n    \"site_admin\": false,\n    \"contributions\": 2022\n  }]";
    private const string RestResultFifty = "[\n  {\n    \"login\": \"allcontributors[bot]\",\n    \"id\": 46447321,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 88,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"jakebolam\",\n    \"id\": 3534236,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 67,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"dependabot[bot]\",\n    \"id\": 49699333,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 30,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"Berkmann18\",\n    \"id\": 8260834,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 19,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"tenshiAMD\",\n    \"id\": 13580338,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 10,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"robertlluberes\",\n    \"id\": 13991439,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 6,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"chrissimpkins\",\n    \"id\": 4249591,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 4,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Jameskmonger\",\n    \"id\": 2037007,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 4,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"greenkeeper[bot]\",\n    \"id\": 23040076,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 4,\n    \"type\": \"Bot\"\n  },\n  {\n    \"login\": \"ben-eb\",\n    \"id\": 1282980,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"bogas04\",\n    \"id\": 6177621,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jamesplease\",\n    \"id\": 2322305,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"mbad0la\",\n    \"id\": 8503331,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Roshanjossey\",\n    \"id\": 8488446,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"MM-coder\",\n    \"id\": 22800592,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 3,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"CassVenere\",\n    \"id\": 47280556,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"ilai-deutel\",\n    \"id\": 10098207,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"itaisteinherz\",\n    \"id\": 22768990,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"sinchang\",\n    \"id\": 3297859,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jfmengels\",\n    \"id\": 3869412,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"jsoref\",\n    \"id\": 2119212,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Frijol\",\n    \"id\": 454690,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"mpeyper\",\n    \"id\": 23029903,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"patcon\",\n    \"id\": 305339,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"sirpeas\",\n    \"id\": 4818642,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"codimiracle\",\n    \"id\": 21952540,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 2,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"0xflotus\",\n    \"id\": 26602940,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"abe-101\",\n    \"id\": 82916197,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"abinoda\",\n    \"id\": 50083,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"atuttle\",\n    \"id\": 46990,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"z0al\",\n    \"id\": 12673605,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alanyee\",\n    \"id\": 1873994,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alejandronanez\",\n    \"id\": 464978,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"alexwlchan\",\n    \"id\": 301220,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"ali-master\",\n    \"id\": 9049092,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"allanbowe\",\n    \"id\": 4420615,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"andrewmcodes\",\n    \"id\": 18423853,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"austinhuang0131\",\n    \"id\": 16656689,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"vedantmgoyal2009\",\n    \"id\": 83997633,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"corneliusroemer\",\n    \"id\": 25161793,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"The24thDS\",\n    \"id\": 26633429,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"DemianD\",\n    \"id\": 5346497,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"e-coders\",\n    \"id\": 83082760,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"EndBug\",\n    \"id\": 26386270,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"King-BR\",\n    \"id\": 51011050,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"fhemberger\",\n    \"id\": 153481,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"guylepage3\",\n    \"id\": 1711854,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"Haroenv\",\n    \"id\": 6270048,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"diegohaz\",\n    \"id\": 3068563,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  },\n  {\n    \"login\": \"nhnb\",\n    \"id\": 364184,\n    \"node_id\": \"U_kgDOAAOr0w\",\n    \"contributions\": 1,\n    \"type\": \"User\"\n  }\n]";

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
        var restClient = new Mock<IRestClient>();
        //Tests where the client returns 3 contributors
        restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = RestResult,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed
            });
        var gitHubRestService = new GitHubRestService(restClient.Object);
        var result = await gitHubRestService.GetRepoContributors("test", "test", 3);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        Assert.That(result, Has.Count.EqualTo(3));
        
        result = await gitHubRestService.GetRepoContributors("test", "test");
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(2));
        Assert.That(result, Has.Count.EqualTo(3));
        
        result = await gitHubRestService.GetRepoContributors("test", "test", 51);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(3));
        Assert.That(result, Has.Count.EqualTo(3));
        
        //Tests where the client returns 50 contributors
        restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = RestResultFifty,
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed
            });
        gitHubRestService = new GitHubRestService(restClient.Object);
        result = await gitHubRestService.GetRepoContributors("test", "test", 3);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(4));
        Assert.That(result, Has.Count.EqualTo(3));
        
        
        result = await gitHubRestService.GetRepoContributors("test", "test", 100);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(6));
        Assert.That(result, Has.Count.EqualTo(100));

        //Tests where the client returns 0 contributors
        restClient.Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = "[]",
                StatusCode = HttpStatusCode.OK,
                IsSuccessStatusCode = true,
                ResponseStatus = ResponseStatus.Completed,
                ContentLength = 0
            });
        gitHubRestService = new GitHubRestService(restClient.Object);
        
        result = await gitHubRestService.GetRepoContributors("test", "test", 100);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(7));
        Assert.That(result, Is.EqualTo(new List<ContributorDto>()));
        
        result = await gitHubRestService.GetRepoContributors("test", "test");
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(8));
        Assert.That(result, Is.EqualTo(new List<ContributorDto>()));
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
        List<HeaderParameter> headers =
        [
            new HeaderParameter("X-RateLimit-Remaining", "0"),
            new HeaderParameter("X-RateLimit-Reset",
                (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 1).ToString())


        ];

        var restClient = new Mock<IRestClient>();
        restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = RestResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers
            });
        var gitHubRestService = new GitHubRestService(restClient.Object);
        var result = await gitHubRestService.GetRepoContributors("test", "test", 3);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        Assert.That(result, Is.EqualTo(new List<ContributorDto>()));
        
        //Tests where the secondary rate limit is exceeded
        headers =
        [
            new HeaderParameter("X-RateLimit-Remaining", "1"),
            new HeaderParameter("Retry-After", "1")

        ];

        restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = RestResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers
            });
        gitHubRestService = new GitHubRestService(restClient.Object);
        result = await gitHubRestService.GetRepoContributors("test", "test", 3);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(2));
        Assert.That(result, Is.EqualTo(new List<ContributorDto>()));
        result = await gitHubRestService.GetRepoContributors("test", "test", 51);
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(4));
        Assert.That(result, Is.EqualTo(new List<ContributorDto>()));
        
        //Tests where the client returns an error
        headers =
        [
            new HeaderParameter("X-RateLimit-Remaining", "1")

        ];

        restClient.Setup(x =>
                x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RestResponse
            {
                Content = RestResult,
                StatusCode = HttpStatusCode.BadRequest,
                IsSuccessStatusCode = false,
                ResponseStatus = ResponseStatus.Error,
                ErrorMessage = "test",
                Headers = headers,
                ErrorException = new NullReferenceException()
            });
        gitHubRestService = new GitHubRestService(restClient.Object);
        Assert.ThrowsAsync<NullReferenceException>(async () => await gitHubRestService.GetRepoContributors("test", "test", 3));
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(5));
        Assert.ThrowsAsync<NullReferenceException>(async () => await gitHubRestService.GetRepoContributors("test", "test", 51));
        restClient.Verify(x => x.ExecuteAsync(It.IsAny<RestRequest>(),
            It.IsAny<CancellationToken>()), Times.Exactly(6));
    }
}