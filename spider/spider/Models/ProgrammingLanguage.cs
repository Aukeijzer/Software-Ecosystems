namespace spider.Models;

public struct ProgrammingLanguage
{
    public ProgrammingLanguage(string name, int size)
    {
        Name = name;
        Size = size;
    }

    public string Name { get; }
    public int Size { get; }
   
}