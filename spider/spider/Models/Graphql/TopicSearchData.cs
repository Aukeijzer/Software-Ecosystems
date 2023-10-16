namespace spider.Models.Graphql;

public class TopicSearchData
{
    public TopicSearch? Topic { get; init; }
    
    public RateLimit RateLimit { get; init; }
}