using SECODashBackend.Models;

namespace SECODashBackend.Classifier;

public static class TopProgrammingLanguagesService
{ 
    // The amount of languages to return
    private const int NumberOfLanguages = 5;

    public static List<EcosystemProgrammingLanguage> GetTopLanguagesForEcosystem(Ecosystem ecosystem)
    {
        // Get the list of languages from the projects in the ecosystem and flatten it
        var languages = ecosystem.Projects.Select(p => p.Languages).SelectMany(l => l);
        // Group the languages by their name and sum their percentages and remove duplicates
        var groupedLanguages = languages.GroupBy(l => l.Language).Select(l => new EcosystemProgrammingLanguage()
        {
            EcosystemId = ecosystem.Id,
            Language = l.Key,
            Percentage = l.Sum(p => p.Percentage)
        }).Distinct();
        
        // Order the languages by their percentage in descending order
        var orderedLanguages = groupedLanguages.Take(NumberOfLanguages).OrderByDescending(l => l.Percentage);
        // Return the top x languages
        return orderedLanguages.ToList();
    }
}