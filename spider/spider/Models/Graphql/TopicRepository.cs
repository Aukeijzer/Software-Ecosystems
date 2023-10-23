namespace spider.Models.Graphql;

public class TopicRepository
{
    public required Repository[] Nodes { get; init; }
    public PageInfo? PageInfo { get; init; }
}