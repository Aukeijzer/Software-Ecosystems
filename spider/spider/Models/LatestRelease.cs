namespace spider.Models;

public class LatestRelease
{
    public string? name { get; set; }
    public Target target { get; set; }
}

public class Target
{
    public History history { get; set; }
}

public class History
{
    public Edges[] edges { get; set; }
}

public class Edges
{
    public Node? node { get; set; }
}

public class Node
{
 public DateTime? committedDate { get; set; }   
}