using spider.Dtos;
using spider.Models;

namespace spider.Converter;

public class GraphqlDataConverter : IGraphqlDataConverter
{
    public List<ProjectDto> SearchToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        List<ProjectDto> projects = DataToProjectDtos(data.search.nodes);
        return projects;
    }
    
    public List<ProjectDto> TopicSearchToProjects(TopicSearchData data)
    {
        List<ProjectDto> projects = DataToProjectDtos(data.topic.repositories.nodes);
        return projects;
    }
    
    public List<ProjectDto> DataToProjectDtos(Repository[] nodes)
    {
        var projects = new List<ProjectDto>();
        foreach (var repository in nodes)
        {
            projects.Add(RepositoryToProject(repository));
        }

        return projects;
    }

    public ProjectDto RepositoryToProject(Repository repository)
    {
        var topics = new string[repository.repositoryTopics.nodes.Length];
        for (var i = 0; i < repository.repositoryTopics.nodes.Length; i++)
        {
            topics[i] = repository.repositoryTopics.nodes[i].topic.name;
        }

        var languages = new ProgrammingLanguageDto[repository.languages.edges.Length];
        for (var i = 0; i < repository.languages.edges.Length; i++)
        {
            float percent = (float)repository.languages.edges[i].size / (float)repository.languages.totalSize * 100f;
            languages[i] = new ProgrammingLanguageDto(repository.languages.edges[i].node.name,percent);
        }

        var project = new ProjectDto
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