using SECODashBackend.Models;
using spider.Models;

namespace spider.Converter;

public class SpiderDataConverter : ISpiderDataConverter
{
    public List<Project> ToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        var projects = new List<Project>();
        foreach (var repository in data.search.nodes)
        {
            projects.Add(new Project()
            {
                Name = repository.name,
                ReadMe = (repository.readme == null ? null : repository.readme.text) ,
                Owner = repository.owner.login
            });
        }
        return projects;
    }
}