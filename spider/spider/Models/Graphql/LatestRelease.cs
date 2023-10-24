namespace spider.Models.Graphql;

public class LatestRelease
{
    public string? Name { get; init; }
    public required Target Target { get; init; }
}

public class Target
{
    public required History History { get; init; }
}

public class History
{
    public required Edges[] Edges { get; init; }
}

public class Edges
{
    public Node? Node { get; init; }
}

public class Node
{
 public DateTime? CommittedDate { get; init; }   
}