namespace spider.Models.Graphql;

/// <summary>
/// The structure of the data returned by the GitHub Graphql API when searching for repository count by keyword
/// </summary>
public class SearchCountData
{
    public SearchResult Search { get; init; }
}