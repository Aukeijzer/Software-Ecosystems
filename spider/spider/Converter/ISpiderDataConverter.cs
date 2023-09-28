using SECODashBackend.Models;
using spider.Models;

namespace spider.Converter;

public interface ISpiderDataConverter
{
    public List<Project> ToProjects(SpiderData search);

    public List<Project> TopicSearchToProjects(TopicSearchData data);

}