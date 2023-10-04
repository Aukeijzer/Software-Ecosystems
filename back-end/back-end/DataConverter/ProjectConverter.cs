using System.Text.RegularExpressions;
using SECODashBackend.Dto;
using SECODashBackend.Enums;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverter;

public static class ProjectConverter
{
    public static Project ToProject(ProjectDto dto)
    {
        var project = new Project
        {
            Id = dto.Id,
            Name = dto.Name,
            CreatedAt = dto.CreatedAt,
            Description = dto.Description,
            Topics = dto.Topics ?? new List<string>(),
            Languages = new List<ProjectProgrammingLanguage>(dto.Languages.Select(ToProjectProgrammingLanguage)),
            NumberOfStars = dto.NumberOfStars,
            Owner = dto.Owner,
            ReadMe = dto.ReadMe
        };
        return project;
    }

    public static ProjectDto ToDto(Project project)
    {
        throw new NotImplementedException();
    }

    private static ProjectProgrammingLanguage ToProjectProgrammingLanguage(ProgrammingLanguageDto dto)
    {
        return new ProjectProgrammingLanguage
        {
            Id = Guid.NewGuid().ToString(),
            Language = ParseProgrammingLanguage(dto.Name),
            Percentage = dto.Percentage
        };
    }

    private static ProgrammingLanguage ParseProgrammingLanguage(string language)
    {
        var trimmedLanguage = Regex.Replace( language, @"\s+", "" );
        try
        {
            return Enum.Parse<ProgrammingLanguage>(trimmedLanguage);
        }
        catch (ArgumentException e)
        {
            return trimmedLanguage switch
            {
                "C++" => ProgrammingLanguage.CPlusPlus,
                "C#" => ProgrammingLanguage.CSharp,
                "F#" => ProgrammingLanguage.FSharp, 
                "Q#" => ProgrammingLanguage.QSharp,
                _ => ProgrammingLanguage.Undefined,
            };
        }
    }
}