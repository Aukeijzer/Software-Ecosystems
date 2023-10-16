
namespace spider.Models;

public class Repository
{
    public string Name { get; init; }
    
    public string Id { get; init; }
    
    public LatestRelease DefaultBranchRef { get; init; }
    
    public DateTime CreatedAt { get; init; }
    public string Description { get; init; }
    
    public int StargazerCount { get; init; }
    public Owner Owner { get; init; }
    public TopicsWrapper RepositoryTopics { get; init; }
    public ReadMe? ReadmeCaps { get; init; }
    public ReadMe? ReadmeLower { get; init; }
    public ReadMe? ReadmeFstCaps { get; init; }
    public ReadMe? ReadmerstCaps { get; init; }
    public ReadMe? ReadmerstLower { get; init; }
    public ReadMe? ReadmerstFstCaps { get; init; }
    public Languages Languages { get; init; }
}
public class RepositoryWrapper
{
    public Repository Repository { get; init; }
    
    public RateLimit RateLimit { get; init; }
}
