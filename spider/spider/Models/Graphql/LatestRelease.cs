namespace spider.Models.Graphql;

public class LatestRelease
{
    public string? Name { get; set; }
    public required Target Target { get; set; }
}

public class Target
{
    public required History History { get; set; }
}

public class History
{
    public required Edges[] Edges { get; set; }
}

public class Edges
{
    public Node? Node { get; set; }
}

public class Node
{
 public DateTime? CommittedDate { get; set; }   
}