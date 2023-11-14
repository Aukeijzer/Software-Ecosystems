using SECODashBackend.Dtos.Project;
using SECODashBackend.Dtos.Topic;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverters;

public static class ProjectConverter
{
    public static Project ToProject(ProjectDto dto)
    {
        return new Project
        {
            Id = dto.Id,
            Name = dto.Name,
            CreatedAt = dto.CreatedAt,
            Description = dto.Description,
            Topics = dto.Topics, 
            Languages = dto.Languages, 
            NumberOfStars = dto.NumberOfStars,
            Owner = dto.Owner,
            ReadMe = dto.ReadMe
        };
    }

    public static ProjectRequestDto ToRequestDto(Project project)
    {
        return new ProjectRequestDto
        {
            OwnerName = project.Owner,
            RepoName = project.Name
        };
    }
    public static TopicRequestDto ToTopicRequestDto(Project project)
    {
        return new TopicRequestDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Readme = project.ReadMe ?? throw new InvalidOperationException()
        };
    }

    public static ProjectDto ToProjectDto(Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            CreatedAt = project.CreatedAt,
            Description = project.Description,
            Topics = project.Topics,
            // Languages = project.Languages, 
            NumberOfStars = project.NumberOfStars,
            Owner = project.Owner,
            ReadMe = project.ReadMe
        };
    }
}