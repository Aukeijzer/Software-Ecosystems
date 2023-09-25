namespace spider.Models;

public class Repository
{
    public string name { get; set; }
    public Owner owner { get; set; }
    public TopicsWrapper repositoryTopics { get; set; }
    public ReadMe readme { get; set; }
}
public class RepositoryWrapper
{
    public Repository repository { get; set; }
}
