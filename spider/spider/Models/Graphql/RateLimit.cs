namespace spider.Models.Graphql;

public class RateLimit
{
    public int? Remaining { get; init; }
    public int? Cost { get; init; }
    public int? Limit { get; init; }
    public int? Used { get; init; }
    public DateTime? ResetAt { get; init; }
}