namespace spider.Models.Graphql;

public class SpiderData
{
    public required SearchResult Search { get; set; }
    
    public RateLimit? RateLimit { get; set; }
}