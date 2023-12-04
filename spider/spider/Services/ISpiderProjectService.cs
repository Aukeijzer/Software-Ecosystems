using Microsoft.AspNetCore.Mvc;
using spider.Dtos;

namespace spider.Services;

public interface ISpiderProjectService
{
    public Task<List<ProjectDto>> GetByKeyword(string name, int amount, string? startCursor);
    public Task<List<ProjectDto>> GetByTopic(string topic, int amount, string? startCursor);
    public Task<ProjectDto> GetByName(string name, string ownerName);
    public Task<List<ProjectDto>> GetByNames(List<ProjectRequestDto> repos);
    public Task<List<ContributorDto>?> GetContributorsByName(string name, string ownerName, int amount);
}