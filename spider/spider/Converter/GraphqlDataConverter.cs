using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Converter;

public class GraphqlDataConverter : IGraphqlDataConverter
{
    public List<ProjectDto> SearchToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        List<ProjectDto> projects = DataToProjectDtos(data.Search.Nodes);
        return projects;
    }
    
    public List<ProjectDto> TopicSearchToProjects(TopicSearchData data)
    {
        List<ProjectDto> projects = DataToProjectDtos(data.Topic.Repositories.Nodes);
        return projects;
    }

    public List<ProjectDto> DataToProjectDtos(Repository[] nodes)
    {
        var projects = new List<ProjectDto>();
        if (nodes != null)
        {
            foreach (var repository in nodes)
            {
                projects.Add(RepositoryToProject(repository));
            }
            
        }
        return projects;
    }

    public ProjectDto RepositoryToProject(Repository repository)
    {
        var topics = new string[repository.RepositoryTopics.Nodes.Length];
        for (var i = 0; i < repository.RepositoryTopics.Nodes.Length; i++)
        {
            topics[i] = repository.RepositoryTopics.Nodes[i].Topic.Name;
        }

        var languages = new ProgrammingLanguageDto[repository.Languages.Edges.Length];
        for (var i = 0; i < repository.Languages.Edges.Length; i++)
        {
            float percent = (float)repository.Languages.Edges[i].Size / (float)repository.Languages.TotalSize * 100f;
            languages[i] = new ProgrammingLanguageDto(repository.Languages.Edges[i].Node.Name,percent);
        }

        string? readme = repository.ReadmeCaps?.Text 
                         ?? repository.ReadmeLower?.Text 
                         ?? repository.ReadmeFstCaps?.Text
                         ?? repository.ReadmerstCaps?.Text
                         ?? repository.ReadmerstLower?.Text
                         ?? repository.ReadmerstFstCaps?.Text;

        var project = new ProjectDto
        {
            Name = repository.Name,
            Id = repository.Id,
            LatestDefaultBranchCommitDate = repository.DefaultBranchRef.Target.History.Edges[0].Node.CommittedDate,
            CreatedAt = repository.CreatedAt,
            ReadMe = readme,
            Owner = repository.Owner.Login,
            NumberOfStars = repository.StargazerCount,
            Description = repository.Description,
            Topics = topics,
            TotalSize = repository.Languages.TotalSize,
            Languages = languages
        };
        return project;
    }
}