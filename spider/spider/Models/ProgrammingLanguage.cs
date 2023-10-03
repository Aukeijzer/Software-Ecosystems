namespace spider.Models;

public struct ProgrammingLanguage
{
    public ProgrammingLanguage(string name, float percentage)
    {
        Name = name;
        Percentage = percentage;
    }

    public string Name { get; }
    public float Percentage { get; }
   
}