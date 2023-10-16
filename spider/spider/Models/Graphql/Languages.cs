namespace spider.Models.Graphql;

public class Languages
{
    public int TotalSize { get; set; }

    public required Language[] Edges { get; set; }
}

public class Language
{
    public int Size { get; set; }
    public required LanguageName Node { get; set; }
}

public class LanguageName
{
    public required string Name { get; set; }
}