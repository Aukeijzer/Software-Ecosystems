namespace spider.Models;

public struct ProgrammingLanguage
{
    public ProgrammingLanguage(string name, float size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public float Size { get; }
   
}