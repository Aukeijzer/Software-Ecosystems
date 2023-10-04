namespace spider.Models;

public class Languages
{
    public int TotalSize { get; set; }

    public Language[] Edges { get; set; }
}

public class Language
{
    public int Size { get; set; }
    public LanguageName Node { get; set; }
}

public class LanguageName
{
    public string Name { get; set; }
}