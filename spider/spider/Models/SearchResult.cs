namespace spider.Models;

public class SearchResult{

    public int? RepositoryCount { get; set; }
    public Repository[] Nodes { get; set; }
}