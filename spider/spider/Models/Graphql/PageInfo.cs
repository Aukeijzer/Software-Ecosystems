namespace spider.Models.Graphql;

public class PageInfo
{
    public string? StartCursor { get; init; }
    public string? EndCursor { get; init; }
    public bool? HasNextPage { get; init; }
}