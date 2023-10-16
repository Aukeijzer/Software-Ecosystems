namespace spider.Models.Graphql;

public class Topic
{
    public required string Name { get; set; }
}

public class TopicWrapper
{
    public required Topic Topic { get; set; }
}
public class TopicsWrapper
{
    public required TopicWrapper[] Nodes { get; set; }
}
