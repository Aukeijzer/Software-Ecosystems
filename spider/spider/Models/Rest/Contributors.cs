namespace spider.Models.Rest;

public class Contributors
{
    public List<Contributor>? Items { get; init; }
}

public class Contributor
{
    public required string Login { get; init; }
    public required string Id { get; init; }
    public required string NodeId { get; init; }
    public int Contributions { get; init; }
    
}