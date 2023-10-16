
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
    public ReadMe? ReadmeCaps { get; set; }
    public ReadMe? ReadmeLower { get; set; }
    public ReadMe? ReadmeFstCaps { get; set; }
    public ReadMe? ReadmerstCaps { get; set; }
    public ReadMe? ReadmerstLower { get; set; }
    public ReadMe? ReadmerstFstCaps { get; set; }
    public Languages Languages { get; set; }
}
public class RepositoryWrapper
{
    public Repository Repository { get; set; }
    
    public RateLimit RateLimit { get; set; }
}
