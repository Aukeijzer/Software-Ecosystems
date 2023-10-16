
namespace spider.Models.Graphql;

public class Repository
{
    public required string Name { get; set; }
    
    public required string Id { get; set; }
    
    public required LatestRelease DefaultBranchRef { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public required string Description { get; set; }
    
    public int StargazerCount { get; set; }
    public required Owner Owner { get; set; }
    public required TopicsWrapper RepositoryTopics { get; set; }
    public ReadMe? ReadmeCaps { get; set; }
    public ReadMe? ReadmeLower { get; set; }
    public ReadMe? ReadmeFstCaps { get; set; }
    public ReadMe? ReadmerstCaps { get; set; }
    public ReadMe? ReadmerstLower { get; set; }
    public ReadMe? ReadmerstFstCaps { get; set; }
    public required Languages Languages { get; set; }
}
public class RepositoryWrapper
{
    public required Repository Repository { get; set; }
    
    public required RateLimit RateLimit { get; set; }
}
