using SECODashBackend.Enums;
using SECODashBackend.Models;

namespace SECODashBackend.Services.ProgrammingLanguages;

public static class TopProgrammingLanguagesService
{ 
    // The amount of languages to return
    private const int NumberOfLanguages = 5;

    public static List<EcosystemProgrammingLanguage> GetTopLanguagesForEcosystem(IEnumerable<Project> projects)
    {
        // Get the list of languages from the projects in the ecosystem and flatten it
        var languages = projects
            .Select(p => p.Languages)
            .SelectMany(l => l);
        
        // Group the languages by their name and sum their percentages and remove duplicates
        var groupedLanguages = languages
            .GroupBy(l => l.Language)
            .Select(l => new EcosystemProgrammingLanguage
                {
                    Language = l.Key,
                    Percentage = l.Sum(p => p.Percentage)
                })
            .Distinct().ToList();
        
        // Get the total sum of all percentages
        float total = groupedLanguages.Select(l => l.Percentage).Sum();

        // Correct the percentages so they add up to 100
        groupedLanguages
            .ForEach(l =>
            {
                l.Percentage = float.Round(l.Percentage / total * 100);
            });
        
        // Order the languages by their percentage in descending order
        var orderedLanguages = groupedLanguages
            .OrderByDescending(l => l.Percentage)
            .Take(NumberOfLanguages)
            .ToList();
        
        var other = new EcosystemProgrammingLanguage
        {
            Language = ProgrammingLanguage.Other,
            Percentage = 100 - orderedLanguages.Select(l => l.Percentage).Sum()
        };
        
        // Add the "Other" language to the list
        orderedLanguages.Add(other);
        // Return the top x languages with the "Other" 
        return orderedLanguages;
    }
}