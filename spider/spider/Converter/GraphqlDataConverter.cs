using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Converter;

public class GraphqlDataConverter : IGraphqlDataConverter
{
    /// <summary>
    /// SearchToProjects converts a SpiderData object to a list of ProjectDto objects.
    /// </summary>
    /// <param name="data">The SpiderData that needs to be converted</param>
    /// <returns>The repositories from data in the form of List&lt;ProjectDto&gt;</returns>
    public List<ProjectDto> SearchToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        List<ProjectDto> projects = DataToProjectDtos(data.Search.Nodes);
        return projects;
    }
    
    /// <summary>
    /// TopicSearchToProjects converts a TopicSearchData object to a list of ProjectDto objects.
    /// </summary>
    /// <param name="data">The TopicSearchData that needs to be converted</param>
    /// <returns>The repositories from data in the form of List&lt;ProjectDto&gt;</returns>
    public List<ProjectDto> TopicSearchToProjects(TopicSearchData data)
    {
        List<ProjectDto> projects = DataToProjectDtos(data.Topic.Repositories.Nodes);
        return projects;
    }

    /// <summary>
    /// DataToProjectDtos converts a Repository[] to a list of ProjectDto objects.
    /// </summary>
    /// <param name="nodes">The Repositories to convert</param>
    /// <returns>The repositories from data in the form of List&lt;ProjectDto&gt;</returns>
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

    
    /// <summary>
    /// RepositoryToProject converts a single Repository to a ProjectDto object.
    /// </summary>
    /// <param name="repository">The repository to convert</param>
    /// <returns>The repository in the form of ProjectDto</returns>
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

        DateTime? mostRecentCommit;
        try
        {
            mostRecentCommit = repository.DefaultBranchRef.Target.History.Edges[0].Node.CommittedDate;
        }
        catch (Exception e)
        {
            mostRecentCommit = null;
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
            LatestDefaultBranchCommitDate = mostRecentCommit,
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