using spider.Dtos;
using spider.Models;
using spider.Models.Rest;

namespace spider.Services;

public interface IGithubRestService
{
    public Task<Contributors> GetRepoContributors(String ownerName, string repoName);
    
}