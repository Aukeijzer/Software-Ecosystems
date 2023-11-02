using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Services.Analysis;

namespace BackendTests;
[TestFixture]
public class EcosystemAnalysisServiceTests
{
    [Test]
    public void GetNormalisedTopXLanguages_ReturnsCorrectList()
    {
        // Arrange
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
        var result = EcosystemAnalysisService.GetNormalisedTopXLanguages(programmingLanguageDtos, 5);
        
        // Assert that the list contains the correct amount of languages
        Assert.That(result, Has.Count.EqualTo(5));
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
}