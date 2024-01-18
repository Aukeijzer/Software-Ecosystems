using FluentAssertions;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Services.Analysis;

namespace BackendTests;
/// <summary>
/// This class contains unit tests for the ElasticsearchAnalysisService.
/// </summary>
[TestFixture]
public class ElasticsearchAnalysisServiceTests
{
    /// <summary>
    /// This tests the SortAndNormalizeLanguages method of the ElasticsearchAnalysisService.
    /// It tests if the method returns the correct list of languages.
    /// We test this by creating a list of ProgrammingLanguageDtos and passing it to the method.
    /// After that we check if the returned list contains the correct amount of languages.
    /// Secondly we check if the total percentage of the returned list is correct.
    /// Lastly we check if the returned list is ordered by descending percentage.
    /// </summary>
    [Test]
    public void SortAndNormalizeLanguages_ReturnsCorrectList()
    {
        // Arrange
        const int numberOfTopLanguages = 5;
        var programmingLanguageDtos = new List<ProgrammingLanguageDto>
        {
            new()
            {
                Language = "C#",
                Percentage = 250
            },
            new()
            {
                Language = "Python",
                Percentage = 200
            },
            new()
            {
                Language = "C",
                Percentage = 150
            },
            new()
            {
                Language = "C++",
                Percentage = 100
            },
            new()
            {
                Language = "Java",
                Percentage = 90
            },
            new()
            {
                Language = "JavaScript",
                Percentage = 80
            }
        };
        
        // Act
        var result = ElasticsearchAnalysisService
            .SortAndNormalizeLanguages(programmingLanguageDtos, numberOfTopLanguages);
        
        // Assert
        
        Assert.Multiple(() =>
        {
            // Assert that the number of languages is correct
            Assert.That(result, Has.Count.EqualTo(numberOfTopLanguages));

            // Assert that the total percentage is correct
            Assert.That(result.Sum(l => l.Percentage), Is.EqualTo(90));
        });

        // Assert that the list is ordered by descending percentage
        Assert.That(result, Is.Ordered.By("Percentage").Descending);
    }
    
    /// <summary>
    /// This tests the FilterSubEcosystems method of the ElasticsearchAnalysisService.
    /// It tests if the method returns the correct list of sub-ecosystems.
    /// We test this by creating a list of SubEcosystemDtos and a list of topics and passing them to the method.
    /// After that we check if the returned list contains the correct sub-ecosystems.
    /// </summary>
    [Test]
    public void FilterSubEcosystems_ReturnsCorrectList()
    {
        // Arrange
        var topics = new List<string> { "agriculture", "crops" };

        var technologies = new List<string> { "apple", "windows" };
        
        var subEcosystemsDtos = new List<SubEcosystemDto>
        {
            new()
            {
                Topic = "agriculture",
                ProjectCount = 6
            },
            new()
            {
                Topic = "crops",
                ProjectCount = 5
            },
            new()
            {
                Topic = "python",
                ProjectCount = 4
            },
            new()
            {
                Topic = "quantum",
                ProjectCount = 3
            },
            new()
            {
                Topic = "machine-learning",
                ProjectCount = 2
            },
            new()
            {
                Topic = "plants",
                ProjectCount = 1
            }
        };

        var expectedResult = new List<SubEcosystemDto>
        {
            new()
            {
                Topic = "quantum",
                ProjectCount = 3
            },
            new()
            {
                Topic = "machine-learning",
                ProjectCount = 2
            },
        };
        
        // Act
        var result = ElasticsearchAnalysisService
            .FilterSubEcosystems(subEcosystemsDtos, topics, technologies)
            .ToList();
        
        // Assert
        result.Should().Equal(expectedResult);
    }
    
    /// <summary>
    /// This tests the SortSubEcosystems method of the ElasticsearchAnalysisService.
    /// It tests if the method returns the correct list of sub-ecosystems.
    /// We test this by creating a list of SubEcosystemDtos and passing them to the method.
    /// After that we check if the returned list is ordered by descending project count.
    /// </summary>
    [Test]
    public void SortSubEcosystems_ReturnsCorrectList()
    {
        // Arrange
        var subEcosystemsDtos = new List<SubEcosystemDto>
        {
            new()
            {
                Topic = "quantum",
                ProjectCount = 4
            },
            new()
            {
                Topic = "crops",
                ProjectCount = 5
            },
            new()
            {
                Topic = "agriculture",
                ProjectCount = 6
            }
        };

        var expectedResult = new List<SubEcosystemDto>
        {
            new()
            {
                Topic = "agriculture",
                ProjectCount = 6
            },
            new()
            {
                Topic = "crops",
                ProjectCount = 5
            },
            new()
            {
                Topic = "quantum",
                ProjectCount = 4
            },
        };
        
        // Act
        var result = ElasticsearchAnalysisService
            .SortSubEcosystems(subEcosystemsDtos)
            .ToList();
        
        // Assert
        result.Should().Equal(expectedResult);
    }
}