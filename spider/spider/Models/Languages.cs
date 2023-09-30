namespace spider.Models;

public class Languages
{
    public int totalSize { get; set; }

    public Language[] edges { get; set; }
}

public class Language
{
    public int size { get; set; }
    public LanguageName node { get; set; }
}

public class LanguageName
{
    public string name { get; set; }
}