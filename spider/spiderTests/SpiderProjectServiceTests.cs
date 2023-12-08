using Moq;
using spider.Converter;
using spider.Dtos;
using spider.Services;


namespace spiderTests;

[TestFixture]
public class SpiderProjectServiceTests
{
    private SpiderProjectService _spiderProjectService;
    private Mock<IGitHubGraphqlService> _mockGitHubGraphqlService;
    private Mock<IGitHubRestService> _mockGitHubRestService;
    private GraphqlDataConverter _graphqlDataConverter;
    private SpiderData output;
    private List<Repository> repositories;
    
    [SetUp]
    public void setup()
    {
        _mockGitHubGraphqlService = new Mock<IGitHubGraphqlService>();
        _mockGitHubRestService = new Mock<IGitHubRestService>();
        _mockGitHubRestService.Setup(x => x.GetRepoContributors(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(new List<ContributorDto>());
        _graphqlDataConverter = new GraphqlDataConverter();
        
        output = new SpiderData();
        repositories = new List<Repository>();
        
        Repository node = new Repository()
        {
            Name = "agriculture",
            Owner = new Owner() { Login = "Seco" },
            Id = "1234", 
            Description = "Agriculture is the science and art of cultivating plants and livestock.",
            DefaultBranchRef = null,
            RepositoryTopics = null,
            Languages = null
        };
        for (int i = 0; i < 25; i++)
        {
            repositories.Add(node);
        }
        output = new SpiderData() { Search = new SearchResult() {Nodes = repositories.ToArray()}};
    }

    [Test]
    public async Task KeywordTest()
    {
        string name = "agriculture";
        int amount = 50;
        string cursor = "Y3Vyc29yOjE=";
        
        _mockGitHubGraphqlService.Setup(x => x.QueryRepositoriesByNameHelper(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync([output,output]);
        _spiderProjectService = new SpiderProjectService(_mockGitHubGraphqlService.Object, _graphqlDataConverter,
            _mockGitHubRestService.Object);
        
        _spiderProjectService.GetByKeyword(name, amount, cursor);
        _mockGitHubGraphqlService.Verify(x => x.QueryRepositoriesByNameHelper(name,
            amount, cursor), Times.Once);

        _mockGitHubRestService.Verify(x => x.GetRepoContributors(It.IsAny<string>()
            , It.IsAny<string>(), It.IsAny<int>()), Times.Once);
    }
}