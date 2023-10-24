using spider.Dtos;
using spider.Models;

namespace spider.Services;

public interface IGithubRestService
{
    public Task<List<ContributorDto>> GetRepoContributors(String ownerName, string repoName);
    
}