using SECODashBackend.Dtos.ProgrammingLanguage;

namespace SECODashBackend.Services.ProgrammingLanguages;

public static class TopProgrammingLanguagesService
{ 
    // The amount of languages to return
    private const int NumberOfLanguages = 5;

    /// <summary>
    /// Converts a list of all the programming languages in an ecosystem with the sum of their usage percentages over
    /// all projects to a "Top x" list, i.e. in descending order of x length with the percentages normalised.
    /// </summary>
    public static List<ProgrammingLanguageDto> GetNormalisedTopXLanguages(
        List<ProgrammingLanguageDto> programmingLanguageDtos, int numberOfTopLanguages)
    {
        programmingLanguageDtos
            .Sort((x, y)  => y.Percentage.CompareTo(x.Percentage));
        var totalSum = programmingLanguageDtos.Sum(l => l.Percentage);
        var topXLanguages = programmingLanguageDtos.Take(numberOfTopLanguages).ToList();
        topXLanguages
            .ForEach(l => l.Percentage = float.Round(l.Percentage / totalSum * 100));
        return topXLanguages;
    }
}