using spider.Dtos;

namespace spider.Services;
/// <summary>
/// IGitHubRestService is an interface for the GitHub rest api.
/// </summary>
public interface IGitHubRestService
{
    /// <summary>
    /// GetRepoContributors sends a rest request to the github api and returns on success and otherwise handles the
    /// error and retries if necessary.
    /// </summary>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <param name="repoName">Name of the repository</param>
    /// <param name="amount">amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?</returns>
    public Task<List<ContributorDto>?> GetRepoContributors(String ownerName, string repoName, int amount = 50);
    
}