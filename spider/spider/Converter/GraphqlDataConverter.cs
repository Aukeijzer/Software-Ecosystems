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
    
    public List<Project> DataToProjects(Repository[] nodes)
    {
        var projects = new List<Project>();
        foreach (var repository in nodes)
        {
            projects.Add(RepositoryToProject(repository));
        }

        return projects;
    }

    public Project RepositoryToProject(Repository repository)
    {
        var topics = new string[repository.repositoryTopics.nodes.Length];
        for (var i = 0; i < repository.repositoryTopics.nodes.Length; i++)
        {
            topics[i] = repository.repositoryTopics.nodes[i].topic.name;
        }

        var languages = new ProgrammingLanguage[repository.languages.edges.Length];
        for (var i = 0; i < repository.languages.edges.Length; i++)
        {
            languages[i] = new ProgrammingLanguage(repository.languages.edges[i].node.name,repository.languages.edges[i].size);
        }

        var project = new Project
        {
            Name = repository.name,
            Id = repository.id,
            ReadMe = repository.readme?.text,
            Owner = repository.owner.login,
            NumberOfStars = repository.stargazerCount,
            Description = repository.description,
            Topics = topics,
            TotalSize = repository.languages.totalSize,
            Languages = languages
        };
        return project;
    }
}