using Moq;
using spider.Dtos;
using spider.Services;


namespace spiderTests;

[TestFixture]
public class SpiderControllerTests
{
    private SpiderController _spiderController;
    private Mock<ISpiderProjectService> _mockSpiderProjectService;

    [SetUp]
    public void Setup()
    {
        _mockSpiderProjectService = new Moq.Mock<ISpiderProjectService>();
    }
    
    [Test]
    public async Task ControllerSearchTest()
    {
        _mockSpiderProjectService.Setup(x => x.GetByKeyword(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync(new List<ProjectDto>());
        
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);
        
        await _spiderController.GetByKeyword("agriculture", 110);
        _mockSpiderProjectService.Verify(x => x.GetByKeyword(It.IsAny<string>(),
            It.IsAny<int>(), It.IsAny<string?>()), Times.Once);
        
        await _spiderController.GetByKeyword("agriculture", 110, "Y3Vyc29yOjE=");
        _mockSpiderProjectService.Verify(x => x.GetByKeyword(It.IsAny<string>(),
            It.IsAny<int>(), It.IsAny<string?>()), Times.Exactly(2));
    }

    [Test]
    public async Task ControllerTopicSearchTest()
    {
        _mockSpiderProjectService.Setup(x => x.GetByTopic(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync(new List<ProjectDto>());
        
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);

        string topic = "agriculture";
        int amount = 110;
        string cursor = "Y3Vyc29yOjE=";
        
        await _spiderController.GetByTopic(topic, amount);
        _mockSpiderProjectService.Verify(x => x.GetByTopic(topic,
            amount, null), Times.Once);
        
        await _spiderController.GetByTopic(topic, amount, cursor);
        _mockSpiderProjectService.Verify(x => x.GetByTopic(topic,
            amount, cursor), Times.Once);
    }
    
    [Test]
    public async Task ControllerNameTest()
    {
        _mockSpiderProjectService.Setup(x => x.GetByName(It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(new ProjectDto() {Name = "agriculture", Owner = "Seco", Id = "123"});
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        
        await _spiderController.GetByName(name, owner);
        _mockSpiderProjectService.Verify(x => x.GetByName(name,
            owner), Times.Once);
    }

    [Test]
    public async Task ControllerNamesTest()
    {
        _mockSpiderProjectService.Setup(x => x.GetByNames(
                It.IsAny<List<ProjectRequestDto>>())).ReturnsAsync(new List<ProjectDto>());
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);
        List<ProjectRequestDto> input = new List<ProjectRequestDto>();
        input.Add(new ProjectRequestDto() {OwnerName = "Seco", RepoName = "agriculture"});
        
        await _spiderController.GetByNames(input);
        _mockSpiderProjectService.Verify(x => x.GetByNames(
            input), Times.Once);
    }
    
    [Test]
    public async Task ControllerContributorsTest()
    {
        _mockSpiderProjectService.Setup(x => x.GetContributorsByName(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new List<ContributorDto>());
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        int amount = 60;
        
        await _spiderController.GetContributorsByName(name, owner, amount);
        _mockSpiderProjectService.Verify(x => x.GetContributorsByName(
            name, owner, amount), Times.Once);
    }

    [Test]
    public async Task ControllerContributorsNullTest()
    {
        List<ContributorDto>? output = null;
        _mockSpiderProjectService.Setup(x => x.GetContributorsByName(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(output);
        _spiderController = new SpiderController(_mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        int amount = 60;
        
        await _spiderController.GetContributorsByName(name, owner, amount);
        _mockSpiderProjectService.Verify(x => x.GetContributorsByName(
            name, owner, amount), Times.Once);
    }
}