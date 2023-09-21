namespace spider.Models;

public class Topic
{
    public string name { get; set; }
}
public class TopicsWrapper
{
    public Topic[] topics { get; set; }
}
