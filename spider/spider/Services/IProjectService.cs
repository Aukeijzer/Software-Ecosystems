using Microsoft.AspNetCore.Mvc;
using spider.Dtos;

namespace spider.Services;

public interface IProjectService
{
    public Task<ActionResult<List<ProjectDto>>> GetByKeyword(string name, int amount, string? startCursor);
    public Task<ActionResult<List<ProjectDto>>> GetByTopic(string topic, int amount, string? startCursor);
    public Task<ActionResult<ProjectDto>> GetByName(string name, string ownerName);
    public Task<ActionResult<List<ProjectDto>>> GetByNames(List<ProjectRequestDto> repos);
    public Task<ActionResult<List<ContributorDto>>> GetContributorsByName(string name, string ownerName, int amount);
}