using RestSharp;
using SECODashBackend.Models;

namespace SECODashBackend.Spider;

public class SpiderService : ISpiderService
{
    public async Task<List<Project>?> GetProjectsByNameAsync(string name)
    {
        var options = new RestClientOptions("https://localhost:7167/Spider");
        var request = new RestRequest(name);
        using var client = new RestClient(options);
        var result = await client.GetAsync<SpiderData>(request);
        return SpiderDataConverter.ToProjects(result);
    }
}

public static class SpiderDataConverter
{
    public static List<Project> ToProjects(SpiderData data)
    {
        var projects = new List<Project>();
        foreach (var repository in data.search.nodes)
        {
            projects.Add(new Project()
            {
                Name = repository.name,
                ReadMe = repository.readmeText,
                Owner = repository.owner.login
            });
        }

        return projects;
    }
}
public class SpiderData
{
    public SearchResult search { get; set; }
}
public class SearchResult
{
    public int repositoryCount { get; set; }
    public Repository[] nodes { get; set; }
}

public class RepositoryWrapper
{
    public Repository repository { get; set; }
}

public class Repository
{
    public string name { get; set; }
    public Owner owner { get; set; }
    public TopicsWrapper repositoryTopics { get; set; }
    public string readmeText { get; set; }
}

public class Owner
{
    public string login { get; set; }
}

public class TopicsWrapper
{
    public Topic[] topics { get; set; }
}

public class Topic
{
    public string name { get; set; }
}
