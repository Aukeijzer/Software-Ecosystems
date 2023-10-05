namespace spider.Models;

public class Topic
{
    public string Name { get; set; }
}

public class TopicWrapper
{
    public Topic Topic { get; set; }
}
public class TopicsWrapper
{
    public TopicWrapper[] Nodes { get; set; }
}
