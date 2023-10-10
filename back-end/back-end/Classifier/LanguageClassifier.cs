using SECODashBackend.Dto;
using SECODashBackend.Models;

namespace SECODashBackend.Classifier;

public class LanguageClassifier
{
    public static List<ProjectProgrammingLanguage> GetLanguagesPerEcosystem(List<Ecosystem> ecosystems)
    {
        // Get the list of projects from the ecosystems 
        List<Project> projectsInEcosystems = ecosystems.SelectMany(e => e.Projects).ToList();
        // Classify the languages
        var top5 = ClassifyLanguages(projectsInEcosystems);
        return top5;
    }

    private static List<ProjectProgrammingLanguage> ClassifyLanguages(List<Project> projectsInEcosystems)
    {
        // The amount of languages to return
        int x = 5;
        
        //The total number of entries
        float total = 0;
        
        // Get the list of languages from the projects in the ecosystem and flatten it
        var languages = projectsInEcosystems.Select(p => p.Languages).ToList().SelectMany(l => l);
        // Group the languages by their name and sum their percentages and remove duplicates
        var groupedLanguages = languages.GroupBy(l => l.Language).Select(l => new ProjectProgrammingLanguage
        {
            Id = Guid.NewGuid().ToString(),
            Language = l.Key,
            Percentage = l.Sum(p => p.Percentage)
        }).Distinct();
        //Fix percentages
        foreach (var projectProgrammingLanguage in groupedLanguages)
        {
            total += projectProgrammingLanguage.Percentage;
        }

        List<ProjectProgrammingLanguage> newList = new List<ProjectProgrammingLanguage>();
        //var finalLanguages = groupedLanguages.Select(language => (language));
        foreach (var pl in groupedLanguages)
        {
            ProjectProgrammingLanguage newPl = new ProjectProgrammingLanguage{Id = pl.Id, Language = pl.Language,Percentage = pl.Percentage / total * 100};
            newList.Add(newPl);
        }
        
        // Order the languages by their percentage in descending order
        var orderedLanguages =  newList.OrderByDescending(l => l.Percentage);
        // Return the top x languages
        return orderedLanguages.Take(x).ToList();
    }
}

