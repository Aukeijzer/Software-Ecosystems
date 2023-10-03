namespace spider.Models;

public struct ProgrammingLanguageDto
{
    public ProgrammingLanguageDto(string name, float percentage)
    {
        Name = name;
        Percentage = percentage;
    }

    public string Name { get; }
    public float Percentage { get; }
}