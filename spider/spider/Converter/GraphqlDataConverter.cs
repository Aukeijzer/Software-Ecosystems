using spider.Models;

namespace spider.Converter;

public class GraphqlDataConverter : IGraphqlDataConverter
{
    public List<Project> SearchToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        List<Project> projects = DataToProjects(data.search.nodes);
        return projects;
    }
    
    public List<Project> TopicSearchToProjects(TopicSearchData data)
    {
        List<Project> projects = DataToProjects(data.topic.repositories.nodes);
        return projects;
    }
    
    private List<Project> DataToProjects(Repository[] nodes)
    {
        var projects = new List<Project>();
        foreach (var repository in nodes)
        {
            string[] topics = new string[repository.repositoryTopics.nodes.Length];
            for (int i = 0; i < repository.repositoryTopics.nodes.Length; i++)
            {
                topics[i] = repository.repositoryTopics.nodes[i].topic.name;
            }
            
            string[] languageNames = new string[repository.languages.edges.Length];
            int[] languageSizes = new int[repository.languages.edges.Length];
            for (int i = 0; i < repository.languages.edges.Length; i++)
            {
                languageNames[i] = repository.languages.edges[i].node.name;
                languageSizes[i] = repository.languages.edges[i].size;
            }
            projects.Add(new Project()
            {
                Name = repository.name,
                Id = repository.id,
                ReadMe = (repository.readme == null ? null : repository.readme.text),
                Owner = repository.owner.login,
                NumberOfStars = repository.stargazerCount,
                Description = repository.description,
                Topics = topics,
                TotalSize = repository.languages.totalSize,
                Languages = languageNames,
                LanguageSizes = languageSizes
            });
        }

        return projects;
    }
}