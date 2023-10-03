using System.Runtime.Serialization;

namespace spider.Models;

public class Repository
{
    public string Name { get; set; }
    
    public string Id { get; set; }
    
    public LatestRelease DefaultBranchRef { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    
    public int StargazerCount { get; set; }
    public Owner Owner { get; set; }
    public TopicsWrapper RepositoryTopics { get; set; }
    public ReadMe? Readme { get; set; }
    public Languages Languages { get; set; }
}
public class RepositoryWrapper
{
    public Repository Repository { get; set; }
}
