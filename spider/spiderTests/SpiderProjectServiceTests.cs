using System.Text.Json;
using Moq;
using spider.Converters;
using spider.Dtos;
using spider.Services;


namespace spiderTests;

[TestFixture]
public class SpiderProjectServiceTests
{
    private SpiderProjectService _spiderProjectService = null!;
    private Mock<IGitHubGraphqlService> _mockGitHubGraphqlService = null!;
    private Mock<IGitHubRestService> _mockGitHubRestService = null!;
    private GraphqlDataConverter _graphqlDataConverter = null!;
    private SpiderData _keywordOutput = null!;
    private TopicSearchData _topicOutput = null!;
    private List<Repository> _repositories = null!;
    private Repository _node = null!;
    
    [SetUp]
    public void Setup()
    {
        _mockGitHubGraphqlService = new Mock<IGitHubGraphqlService>();
        _mockGitHubRestService = new Mock<IGitHubRestService>();
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new List<ContributorDto>());
        _graphqlDataConverter = new GraphqlDataConverter();
        
        _keywordOutput = new SpiderData();
        _topicOutput = new TopicSearchData();
        _repositories = new List<Repository>();
        
        _node = new Repository()
        {
            Name = "agriculture",
            Owner = new Owner() { Login = "Seco" },
            Id = "1234", 
            Description = "Agriculture is the science and art of cultivating plants and livestock.",
            DefaultBranchRef = null!,
            RepositoryTopics = new TopicsWrapper() {Nodes = Array.Empty<TopicWrapper>()},
            Languages = new Languages() {Edges = Array.Empty<Language>(), TotalSize = 0}
        };
        for (int i = 0; i < 25; i++)
        {
            _repositories.Add(_node);
        }
        _keywordOutput = new SpiderData() { Search = new SearchResult() {Nodes = _repositories.ToArray()}};
        _topicOutput = new TopicSearchData() { Topic = new TopicSearch() {Repositories = new TopicRepository()
        {
            Nodes = _repositories.ToArray()
        }}};
    }

    /// <summary>
    /// This tests the GetByKeyword method of the SpiderProjectService.
    /// It tests if the method calls the correct services.
    /// We test this by creating a mock of the GitHubGraphqlService and the GitHubRestService.
    /// After that we call the GetByKeyword method in the SpiderProjectService.
    /// Lastly we check if the correct methods have been called the correct amount of times.
    /// </summary>
    [Test]
    public async Task KeywordTest()
    {
        string name = "agriculture";
        int amount = 50;
        string cursor = "Y3Vyc29yOjE=";
        
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByNameHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .ReturnsAsync([_keywordOutput,_keywordOutput]);
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        await _spiderProjectService.GetByKeyword(name, amount, cursor);
        _mockGitHubGraphqlService.Verify(x => x.QueryRepositoriesByNameHelper(name,
            amount, cursor), Times.Once);

        _mockGitHubRestService.Verify(x => x.GetRepoContributors(It.IsAny<string>()
            , It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(50));
    }

    /// <summary>
    /// This tests the GetByKeyword method of the SpiderProjectService.
    /// It tests if the method handles the errors correctly.
    /// We test this by creating a mock of the GitHubGraphqlService and the GitHubRestService.
    /// After that we call the GetByKeyword method in the SpiderProjectService.
    /// Lastly we check if the correct errors are thrown.
    /// </summary>
    [Test]
    public Task KeywordErrorTests()
    {
        string name = "agriculture";
        int amount = 50;
        string cursor = "Y3Vyc29yOjE=";
        
        //Test with JsonException
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByNameHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new JsonException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<HttpRequestException>(async () => 
            await _spiderProjectService.GetByKeyword(name, amount, cursor));
        
        //Test with NullReferenceException
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByNameHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new NullReferenceException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<HttpRequestException>(async () => 
            await _spiderProjectService.GetByKeyword(name, amount, cursor));
        
        //Test with Exception
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByNameHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new Exception());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<Exception>(async () => 
            await _spiderProjectService.GetByKeyword(name, amount, cursor));
        return Task.CompletedTask;
    }

    /// <summary>
    /// This tests the GetByTopic method of the SpiderProjectService.
    /// It tests if the method calls the correct services.
    /// We test this by creating a mock of the GitHubGraphqlService and the GitHubRestService.
    /// After that we call the GetByTopic method in the SpiderProjectService.
    /// Lastly we check if the correct methods have been called the correct amount of times.
    /// </summary>
    [Test]
    public async Task TopicTests()
    {
        string topic = "agriculture";
        int amount = 50;
        string cursor = "Y3Vyc29yOjE=";
        
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByTopicHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .ReturnsAsync([_topicOutput,_topicOutput]);
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        await _spiderProjectService.GetByTopic(topic, amount, cursor);
        _mockGitHubGraphqlService.Verify(x => x.QueryRepositoriesByTopicHelper(topic,
            amount, cursor), Times.Once);

        _mockGitHubRestService.Verify(x => x.GetRepoContributors(It.IsAny<string>()
            , It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(50));
    }
    
    /// <summary>
    /// This tests the GetByTopic method of the SpiderProjectService.
    /// It tests if the method handles the errors correctly.
    /// We test this by creating a mock of the GitHubGraphqlService and the GitHubRestService.
    /// After that we call the GetByTopic method in the SpiderProjectService.
    /// Lastly we check if the correct errors are thrown.
    /// </summary>
    [Test]
    public Task TopicErrorTests()
    {
        string topic = "agriculture";
        int amount = 50;
        string cursor = "Y3Vyc29yOjE=";
        
        //Test with JsonException
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByTopicHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new JsonException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<HttpRequestException>(async () => 
            await _spiderProjectService.GetByTopic(topic, amount, cursor));
        
        //Test with NullReferenceException
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByTopicHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new NullReferenceException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<HttpRequestException>(async () => 
            await _spiderProjectService.GetByTopic(topic, amount, cursor));
        
        //Test with Exception
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByTopicHelper(It.IsAny<string>(),
                50, It.IsAny<string?>()))
            .Throws(new Exception());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        Assert.ThrowsAsync<Exception>(async () => 
            await _spiderProjectService.GetByTopic(topic, amount, cursor));
        return Task.CompletedTask;
    }

    /// <summary>
    /// This tests the GetByName method of the SpiderProjectService.
    /// It tests if the method calls the correct services.
    /// We test this by creating a mock of the GitHubGraphqlService.
    /// After that we call the GetByName method in the SpiderProjectService.
    /// Lastly we check if the correct methods have been called the correct amount of times.
    /// </summary>
    [Test]
    public async Task NameTests()
    {
        string name = "agriculture";
        string owner = "Seco";
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoryByName(It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(new RepositoryWrapper() {Repository = _node});
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        await _spiderProjectService.GetByName(name, owner);
        _mockGitHubGraphqlService.Verify(x => x.QueryRepositoryByName(name, owner), Times.Once);
    }

    /// <summary>
    /// This tests the GetByName method of the SpiderProjectService.
    /// It tests if the method calls the correct services.
    /// We test this by creating a mock of the GitHubGraphqlService.
    /// After that we call the GetByName method in the SpiderProjectService.
    /// Lastly we check if the correct methods have been called the correct amount of times.
    /// </summary>
    [Test]
    public async Task NamesTests()
    {
        ProjectRequestDto input = new ProjectRequestDto()
        {
            RepoName = "agriculture",
            OwnerName = "Seco"
        };
        _mockGitHubGraphqlService.Setup(x => x.GetByNames(It.IsAny<List<ProjectRequestDto>>()))
            .ReturnsAsync([_keywordOutput,_keywordOutput]);
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        await _spiderProjectService.GetByNames([input]);
        _mockGitHubGraphqlService.Verify(x => x.GetByNames(It.IsAny<List<ProjectRequestDto>>()), Times.Once);
    }
    
    /// <summary>
    /// This tests the GetContributorsByName method of the SpiderProjectService.
    /// It tests if the method calls the correct services.
    /// We test this by creating a mock of the GitHubRestService.
    /// After that we call the GetContributorsByName method in the SpiderProjectService.
    /// Lastly we check if the correct methods have been called the correct amount of times.
    /// </summary>
    [Test]
    public async Task ContributorTests()
    {
        string repoName = "agriculture";
        string ownerName = "Seco";
        int amount = 50;
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new List<ContributorDto>());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        await _spiderProjectService.GetContributorsByName(repoName, ownerName, amount);
        _mockGitHubRestService.Verify(x => x.GetRepoContributors(ownerName,
            repoName, amount), Times.Once);
    }

    /// <summary>
    /// This tests the GetContributorsByName method of the SpiderProjectService.
    /// It tests if the method handles the errors correctly.
    /// We test this by creating a mock of the GitHubRestService.
    /// After that we call the GetContributorsByName method in the SpiderProjectService.
    /// Lastly we check if the correct errors are thrown.
    /// </summary>
    [Test]
    public Task ContributorErrorTests()
    {
        string repoName = "agriculture";
        string ownerName = "Seco";
        int amount = 50;
        
        //Test with JsonException
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<int>())).Throws(new JsonException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        Assert.ThrowsAsync<HttpRequestException>(async () => await _spiderProjectService.GetContributorsByName(
            repoName, ownerName, amount));

        //Test with NullReferenceException
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<int>())).Throws(new NullReferenceException());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        Assert.ThrowsAsync<HttpRequestException>(async () => await _spiderProjectService.GetContributorsByName(
            repoName, ownerName, amount));
        
        //Test with Exception
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(),
            It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception());
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        Assert.ThrowsAsync<Exception>(async () => await _spiderProjectService.GetContributorsByName(repoName,
            ownerName, amount));
        return Task.CompletedTask;
    }
}