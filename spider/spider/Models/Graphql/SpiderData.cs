namespace spider.Models.Graphql;

/// <summary>
/// The structure of the data returned by the GitHub Graphql API when searching for repositories by name
/// </summary>
public class SpiderData
{
    public SearchResult Search { get; init; }
    
    public RateLimit? RateLimit { get; init; }
}