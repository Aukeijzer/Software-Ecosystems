namespace spider.Models.Graphql;

public class SearchResult{

    public int? RepositoryCount { get; set; }
    public required Repository[] Nodes { get; set; }
}