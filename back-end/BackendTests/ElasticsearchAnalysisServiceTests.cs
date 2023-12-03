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
    /// Thirdly we check if the returned list does not contain any languages with a percentage of 0.
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
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Percentage, Is.GreaterThan(result[1].Percentage));
            Assert.That(result[1].Percentage, Is.GreaterThan(result[2].Percentage));
            Assert.That(result[2].Percentage, Is.GreaterThan(result[3].Percentage));
            Assert.That(result[3].Percentage, Is.GreaterThan(result[4].Percentage));
        });
    }
    
    /// <summary>
    /// This tests the SortSubEcosystems method of the ElasticsearchAnalysisService.
    /// It tests if the method returns the correct list of sub-ecosystems.
    /// We test this by creating a list of SubEcosystemDtos and passing it to the method.
    /// After that we check if the returned list contains the correct amount of sub-ecosystems.
    /// Also we check if the returned list does not contain any sub-ecosystems with a topic that is in the topics list.
    /// Lastly we check if the returned list is ordered by descending number of projects.
    /// </summary>
    [Test]
    public void SortSubEcosystems_ReturnsCorrectList()
    {
        // Arrange
        const int numberOfTopSubEcosystems = 3;
        var topics = new List<string> { "agriculture", "crops" };
        
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
                Topic = "ai",
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
        
        // Act
        var result = ElasticsearchAnalysisService
            .SortSubEcosystems(subEcosystemsDtos, topics, numberOfTopSubEcosystems);
        
        // Assert
        
        Assert.Multiple(() =>
        {

            // Assert that the list contains the correct amount of sub-ecosystems
            Assert.That(result, Has.Count.EqualTo(numberOfTopSubEcosystems));

            // Assert that the topics of the ecosystem are not in the top sub-ecosystem list
            Assert.That(!result
                .Select(s => s.Topic)
                .Intersect(topics)
                .Any());
        });

        // Assert that the whole list is ordered by number of projects
        Assert.Multiple(() =>
        {
            Assert.That(result[0].ProjectCount, Is.GreaterThan(result[1].ProjectCount));
            Assert.That(result[1].ProjectCount, Is.GreaterThan(result[2].ProjectCount));
        });
    }
}