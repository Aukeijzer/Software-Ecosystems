using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Services.Analysis;

namespace BackendTests;
[TestFixture]
public class ElasticsearchAnalysisServiceTests
{
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
            },
            new()
            {
                Language = "TypeScript",
                Percentage = 70
            },
            new()
            {
                Language = "Shell",
                Percentage = 60
            },
        };
        
        // Act
        var result = ElasticsearchAnalysisService
            .SortAndNormalizeLanguages(programmingLanguageDtos, numberOfTopLanguages);
        
        // Assert that the list contains the correct amount of languages
        Assert.That(result, Has.Count.EqualTo(numberOfTopLanguages));
        // Assert that the total percentage is correct
        Assert.That(result.Sum(l => l.Percentage), Is.EqualTo(79));
        
        // Assert that the whole list is ordered by percentage
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Percentage, Is.GreaterThan(result[1].Percentage));
            Assert.That(result[1].Percentage, Is.GreaterThan(result[2].Percentage));
            Assert.That(result[2].Percentage, Is.GreaterThan(result[3].Percentage));
            Assert.That(result[3].Percentage, Is.GreaterThan(result[4].Percentage));
        });
    }

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
            .SortSubEcosystems(subEcosystemsDtos, topics,  numberOfTopSubEcosystems);
        
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