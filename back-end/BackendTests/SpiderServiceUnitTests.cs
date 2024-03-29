﻿// Copyright (C) <2024>  <ODINDash>
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

using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Spider;

namespace BackendTests;
/// <summary>
/// This class contains unit tests for the SpiderService.
/// </summary>
[TestFixture]
public class SpiderServiceUnitTests
{
    private readonly SpiderService testSpiderService = new SpiderService("http://localhost:5205/Spider");
    
    /// <summary>
    /// This tests the GetProjectsByTopicAsync method of the SpiderService.
    /// It tests if the method returns the correct amount of projects.
    /// Lastly it checks if the returned list contains projects with the given topic.
    /// </summary>
    [Test]
    public void GetProjectsByTopicAsync_ReturnsProjects()
    {
        // Arrange
        string testTopic = "ai";
        int amount = 10;
        
        // Act
        Task<List<ProjectDto>> result = testSpiderService.GetProjectsByTopicAsync(testTopic, amount);
        
        // Assert
        var returnList = result.Result;
        Assert.That(result.Exception, Is.Null);
        Assert.That(returnList, Is.Not.Null);
        Assert.That(returnList.Count, Is.EqualTo(amount));
        Assert.IsNotEmpty(returnList.FindAll(p => p.Topics.Contains(testTopic)));
    }
    
    /// <summary>
    /// This tests the GetProjectsByKeywordAsync method of the SpiderService.
    /// It tests if the method returns the correct amount of projects.
    /// Lastly it checks if the returned list contains projects with the given keyword.
    /// </summary>
    [Test]
    public void GetProjectsByKeywordAsync_ReturnsProjects()
    {
        // Arrange
        string testKeyword = "quantum";
        int amount = 10;
        
        // Act
        Task<List<ProjectDto>> result = testSpiderService.GetProjectsByKeywordAsync(testKeyword, amount);
        
        // Assert
        var returnList = result.Result;
        Assert.That(result.Exception, Is.Null);
        Assert.That(returnList, Is.Not.Null);
        Assert.That(returnList.Count, Is.EqualTo(amount));
        Assert.IsNotEmpty(returnList.FindAll(p => p.Name.Contains(testKeyword)));
    }
}