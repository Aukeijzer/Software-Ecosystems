
namespace spider.Models.Graphql;

public class Repository
{
    public required string Name { get; init; }
    
    public required string Id { get; init; }
    
    public required LatestRelease DefaultBranchRef { get; init; }
    
    public DateTime CreatedAt { get; init; }
    public required string Description { get; init; }
    
    public int StargazerCount { get; init; }
    public required Owner Owner { get; init; }
    public required TopicsWrapper RepositoryTopics { get; init; }
    public ReadMe? ReadmeCaps { get; init; }
    public ReadMe? ReadmeLower { get; init; }
    public ReadMe? ReadmeFstCaps { get; init; }
    public ReadMe? ReadmerstCaps { get; init; }
    public ReadMe? ReadmerstLower { get; init; }
    public ReadMe? ReadmerstFstCaps { get; init; }
    public required Languages Languages { get; init; }
}
public class RepositoryWrapper
{
    public Repository Repository { get; init; }
    
    public RateLimit RateLimit { get; init; }
}
