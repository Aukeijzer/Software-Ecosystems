namespace spider.Dtos;

public struct ProgrammingLanguageDto
{
    public ProgrammingLanguageDto(string language, float percentage)
    {
        Language = language;
        Percentage = percentage;
    }

    public string Language { get; }
    public float Percentage { get; }
}