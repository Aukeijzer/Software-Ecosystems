using SECODashBackend.Models;
using System.Linq;

namespace SECODashBackend.Classifier;

public static class TopProgrammingLanguagesService
{ 
    // The amount of languages to return
    private const int NumberOfLanguages = 5;
    // total size
    private static float total = 0;

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

        //var total = Sum(groupedLanguages.Select(l => l.Percentage));

        foreach (var pl in groupedLanguages)
        {
            total += pl.Percentage;
        }

        var fixedPercentages = groupedLanguages.Select(l => new EcosystemProgrammingLanguage()
        {
            EcosystemId = l.EcosystemId,
            Language = l.Language,
            Percentage = l.Percentage / total * 100
        });
        
        // Order the languages by their percentage in descending order
        var orderedLanguages = fixedPercentages.Take(NumberOfLanguages).OrderByDescending(l => l.Percentage);
        // Return the top x languages
        return orderedLanguages.ToList();
    }
}