namespace spider.Models;

public class Topic
{
    public string name { get; set; }
}

public class TopicWrapper
{
    public Topic topic { get; set; }
}
public class TopicsWrapper
{
    public TopicWrapper[] nodes { get; set; }
}
