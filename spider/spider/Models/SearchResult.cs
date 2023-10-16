namespace spider.Models;

public class SearchResult{

    public int? RepositoryCount { get; init; }
    public Repository[] Nodes { get; init; }
}