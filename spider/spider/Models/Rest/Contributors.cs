namespace spider.Models.Rest;

public class Contributors
{
    public List<Contributor>? Items { get; set; }
}

public class Contributor
{
    public required string Login { get; set; }
    public required string Id { get; set; }
    public required string NodeId { get; set; }
    public int Contributions { get; set; }
    
}