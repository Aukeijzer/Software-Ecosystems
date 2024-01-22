namespace spider.Models.Graphql;

/// <summary>
/// The structure of the data returned by the GitHub Graphql API when searching for how many repositories are in a search
/// </summary>
public class RepositoryCountData
{
    public int? RepositoryCount { get; init; }
}