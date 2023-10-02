using spider.Models;

namespace spider.Converter;

public interface IGraphqlDataConverter
{
    public List<Project> SearchToProjects(SpiderData search);

    public List<Project> TopicSearchToProjects(TopicSearchData data);

    public Project RepositoryToProject(Repository repository);

}