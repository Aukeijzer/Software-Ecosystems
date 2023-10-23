namespace spider.Models.Graphql;

public class SearchResult{

    public int? RepositoryCount { get; init; }
    public PageInfo? PageInfo { get; init; }
    public required Repository[] Nodes { get; init; }
}