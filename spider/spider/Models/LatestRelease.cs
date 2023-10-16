namespace spider.Models;

public class LatestRelease
{
    public string? Name { get; init; }
    public Target Target { get; init; }
}

public class Target
{
    public History History { get; init; }
}

public class History
{
    public Edges[] Edges { get; init; }
}

public class Edges
{
    public Node? Node { get; init; }
}

public class Node
{
 public DateTime? CommittedDate { get; init; }   
}