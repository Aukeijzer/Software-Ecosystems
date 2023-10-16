namespace spider.Models.Graphql;

public class TopicSearchData
{
    public TopicSearch? Topic { get; set; }
    
    public required RateLimit RateLimit { get; set; }
}