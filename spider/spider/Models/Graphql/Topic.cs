namespace spider.Models.Graphql;

public class Topic
{
    public required string Name { get; init; }
}

public class TopicWrapper
{
    public required Topic Topic { get; init; }
}
public class TopicsWrapper
{
    public required TopicWrapper[] Nodes { get; init; }
}
