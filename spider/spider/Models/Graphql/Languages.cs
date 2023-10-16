namespace spider.Models.Graphql;

public class Languages
{
    public int TotalSize { get; init; }

    public required Language[] Edges { get; init; }
}

public class Language
{
    public int Size { get; init; }
    public required LanguageName Node { get; init; }
}

public class LanguageName
{
    public required string Name { get; init; }
}