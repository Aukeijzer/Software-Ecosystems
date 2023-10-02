namespace spider.Models;

public class Repository
{
    public string name { get; set; }
    
    public string id { get; set; }
    public string description { get; set; }
    
    public int stargazerCount { get; set; }
    public Owner owner { get; set; }
    public TopicsWrapper repositoryTopics { get; set; }
    public ReadMe? readme { get; set; }
    public Languages languages { get; set; }
}
public class RepositoryWrapper
{
    public Repository repository { get; set; }
}
