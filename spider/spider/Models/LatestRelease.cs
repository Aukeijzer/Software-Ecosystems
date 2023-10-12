namespace spider.Models;

public class LatestRelease
{
    public string? Name { get; set; }
    public Target Target { get; set; }
}

public class Target
{
    public History History { get; set; }
}

public class History
{
    public Edges[] Edges { get; set; }
}

public class Edges
{
    public Node? Node { get; set; }
}

public class Node
{
 public DateTime? CommittedDate { get; set; }   
}