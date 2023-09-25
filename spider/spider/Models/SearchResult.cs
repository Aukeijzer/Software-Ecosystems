namespace spider.Models;

public class SearchResult{

    public int repositoryCount { get; set; }
    public Repository[] nodes { get; set; }
}