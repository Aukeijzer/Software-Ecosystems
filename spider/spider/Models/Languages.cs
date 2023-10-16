namespace spider.Models;

public class Languages
{
    public int TotalSize { get; init; }

    public Language[] Edges { get; init; }
}

public class Language
{
    public int Size { get; init; }
    public LanguageName Node { get; init; }
}

public class LanguageName
{
    public string Name { get; init; }
}