using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Converter;

public interface IGraphqlDataConverter
{
    public List<ProjectDto> SearchToProjects(SpiderData search);

    public List<ProjectDto> TopicSearchToProjects(TopicSearchData data);

    public ProjectDto RepositoryToProject(Repository repository);

}