namespace spider.Models;

public class Topic
{
    public string Name { get; init; }
}

public class TopicWrapper
{
    public Topic Topic { get; init; }
}
public class TopicsWrapper
{
    public TopicWrapper[] Nodes { get; init; }
}
