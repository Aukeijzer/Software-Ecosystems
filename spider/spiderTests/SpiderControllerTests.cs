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

using Moq;
using spider.Dtos;
using spider.Services;


namespace spiderTests;

[TestFixture]
public class SpiderControllerTests
{
    
    /// <summary>
    /// This tests the GetByKeyword method of the SpiderController.
    /// It tests if the method calls the GetByKeyword method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService and calling the GetByKeyword method of the SpiderController.
    /// After that we verify if the GetByKeyword method of the SpiderProjectService has been called.
    /// Lastly we call the GetByKeyword method of the SpiderController with an additional cursor parameter.
    /// After that we verify if the GetByKeyword method of the SpiderProjectService has been called again.
    /// </summary>
    [Test]
    public async Task ControllerSearchTest()
    {
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetByKeywordSplit(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync(new List<ProjectDto>());
        
        var spiderController = new SpiderController(mockSpiderProjectService.Object);
        
        await spiderController.GetByKeyword("agriculture", 110);
        mockSpiderProjectService.Verify(x => x.GetByKeywordSplit(It.IsAny<string>(),
            It.IsAny<int>(), It.IsAny<string?>()), Times.Once);
        
        await spiderController.GetByKeyword("agriculture", 110, "Y3Vyc29yOjE=");
        mockSpiderProjectService.Verify(x => x.GetByKeywordSplit(It.IsAny<string>(),
            It.IsAny<int>(), It.IsAny<string?>()), Times.Exactly(2));
    }

    /// <summary>
    /// This tests the GetByTopic method of the SpiderController.
    /// It tests if the method calls the GetByTopic method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService and calling the GetByTopic method of the SpiderController.
    /// After that we verify if the GetByTopic method of the SpiderProjectService has been called.
    /// Lastly we call the GetByTopic method of the SpiderController with an additional cursor parameter.
    /// After that we verify if the GetByTopic method of the SpiderProjectService has been called again.
    /// </summary>
    [Test]
    public async Task ControllerTopicSearchTest()
    {
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetByTopic(It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<string?>()))
            .ReturnsAsync(new List<ProjectDto>());
        
        var spiderController = new SpiderController(mockSpiderProjectService.Object);

        string topic = "agriculture";
        int amount = 110;
        string cursor = "Y3Vyc29yOjE=";
        
        await spiderController.GetByTopic(topic, amount);
        mockSpiderProjectService.Verify(x => x.GetByTopic(topic,
            amount, null), Times.Once);
        
        await spiderController.GetByTopic(topic, amount, cursor);
        mockSpiderProjectService.Verify(x => x.GetByTopic(topic,
            amount, cursor), Times.Once);
    }
    
    /// <summary>
    /// This tests the GetByName method of the SpiderController.
    /// It tests if the method calls the GetByName method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService and calling the GetByName method of the SpiderController.
    /// After that we verify if the GetByName method of the SpiderProjectService has been called.
    /// </summary>
    [Test]
    public async Task ControllerNameTest()
    {
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetByName(It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(new ProjectDto() {Name = "agriculture", Owner = "Seco", Id = "123"});
        var spiderController = new SpiderController(mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        
        await spiderController.GetByName(name, owner);
        mockSpiderProjectService.Verify(x => x.GetByName(name,
            owner), Times.Once);
    }

    /// <summary>
    /// This tests the GetByNames method of the SpiderController.
    /// It tests if the method calls the GetByNames method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService and calling the GetByNames method of the SpiderController.
    /// After that we verify if the GetByNames method of the SpiderProjectService has been called.
    /// </summary>
    [Test]
    public async Task ControllerNamesTest()
    {
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetByNames(
                It.IsAny<List<ProjectRequestDto>>())).ReturnsAsync(new List<ProjectDto>());
        var spiderController = new SpiderController(mockSpiderProjectService.Object);
        List<ProjectRequestDto> input =
        [
            new ProjectRequestDto() { OwnerName = "Seco", RepoName = "agriculture" }

        ];

        await spiderController.GetByNames(input);
        mockSpiderProjectService.Verify(x => x.GetByNames(
            input), Times.Once);
    }
    
    /// <summary>
    /// This tests the GetContributorsByName method of the SpiderController.
    /// It tests if the method calls the GetContributorsByName method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService and calling the GetContributorsByName method of the SpiderController.
    /// After that we verify if the GetContributorsByName method of the SpiderProjectService has been called.
    /// </summary>
    [Test]
    public async Task ControllerContributorsTest()
    {
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetContributorsByName(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(new List<ContributorDto>());
        var spiderController = new SpiderController(mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        int amount = 60;

        await spiderController.GetContributorsByName(name, owner, amount);
        mockSpiderProjectService.Verify(x => x.GetContributorsByName(
            name, owner, amount), Times.Once);
    }

    /// <summary>
    /// This tests the GetContributorsByName method of the SpiderController.
    /// It tests if the method calls the GetContributorsByName method of the SpiderProjectService.
    /// We test this by setting up a mock SpiderProjectService that returns null and calling the GetContributorsByName
    /// method of the SpiderController.
    /// After that we verify if the GetContributorsByName method of the SpiderProjectService has been called.
    /// </summary>
    [Test]
    public async Task ControllerContributorsNullTest()
    {
        List<ContributorDto>? output = null;
        var mockSpiderProjectService = new Mock<ISpiderProjectService>();
        mockSpiderProjectService.Setup(x => x.GetContributorsByName(
            It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(output);
        var spiderController = new SpiderController(mockSpiderProjectService.Object);
        
        string name = "agriculture";
        string owner = "Seco";
        int amount = 60;
        
        await spiderController.GetContributorsByName(name, owner, amount);
        mockSpiderProjectService.Verify(x => x.GetContributorsByName(
            name, owner, amount), Times.Once);
    }
}