namespace spider.Models.Graphql;

/// <summary>
/// The structure of the data returned by the GitHub Graphql API when searching for repositories by topic
/// </summary>
public class TopicSearchData
{
    public TopicSearch? Topic { get; init; }
    
    public RateLimit RateLimit { get; init; }
}