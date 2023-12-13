namespace spider.Dtos;

/// <summary>
/// A data transfer object for a programming language
/// </summary>
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