// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

using System.Net;
using System.Text.Json;
using spider.Converters;
using spider.Dtos;

namespace spider.Services;

/// <summary>
/// SpiderProjectService is the service that handles all requests concerning repositories. It uses the GitHubGraphqlService
/// and the GitHubRestService to get the data it needs.
/// </summary>
public class SpiderProjectService : ISpiderProjectService
{
    private readonly ILogger<SpiderProjectService> _logger;
    private readonly IGitHubGraphqlService _gitHubGraphqlService;
    private readonly IGraphqlDataConverter _graphqlDataConverter;
    private readonly IGitHubRestService _gitHubRestService;
    
    public SpiderProjectService(IGitHubGraphqlService gitHubGraphqlService, IGraphqlDataConverter graphqlDataConverter,
        IGitHubRestService gitHubRestService)
    {
        _logger = new Logger<SpiderProjectService>(new LoggerFactory());
        _gitHubGraphqlService = gitHubGraphqlService;
        _graphqlDataConverter = graphqlDataConverter;
        _gitHubRestService = gitHubRestService;
    }

    /// <summary>
    /// GetByKeywordSplit splits the search space into smaller chunks and then calls GetByKeyword on each chunk.
    /// </summary>
    /// <param name="name">keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    ///     the start</param>
    /// <returns>A list of repositories including contributors in the form of List&lt;ProjectDto&gt;</returns>
    public async Task<List<ProjectDto>> GetByKeywordSplit(string name, int amount, string? startCursor)
    {
        //set max value
        var mostStarred = await _gitHubGraphqlService.QueryRepositoriesByName(name + " sort:desc", 1, startCursor);
        if (mostStarred.Search.Nodes.Length == 0)
        {
            return new List<ProjectDto>();
        }
        var max = mostStarred.Search.Nodes[0].StargazerCount;
        //Use binary split to split the search space into smaller chunks
        var sections = await BinarySplit(name, 5, max);
        sections.Reverse();
        var split = new Queue<(int,int)>(sections);
        var result = new List<ProjectDto>();
        
        while (amount > result.Count && split.Count > 0)
        {
            var (lower, upper) = split.Dequeue();
            result.AddRange(await GetByKeyword(name + " stars:" + lower + ".." + upper, Math.Min(1000,amount-result.Count), startCursor));
        }
        
        return result;
    }

    /// <summary>
    /// BinarySplit splits the search space into smaller chunks by using an algorithm based on binary search.
    /// </summary>
    /// <param name="name">keyword to search by</param>
    /// <param name="min">The lower bound of the search space</param>
    /// <param name="max">The upper bound of the search space</param>
    /// <returns>Returns a list of lower and upper bounds that each contain no more than 1000 repositories</returns>
    private async Task<List<(int, int)>> BinarySplit(string name, int min, int max)
    {
        // If min and max are equal we can't divide further
        if (min == max)
        {
            return [(min,max)];
        }
        
        var repoCount = await _gitHubGraphqlService.GetRepoCount(name + " sort:desc ", min, max);
        // If there are no repositories in the range we can't divide further
        if (repoCount is null or 0)
        {
            return [(min,max)];
        }
        
        // If there are more than 1000 repositories in the range we divide further
        if (repoCount > 1000)
        {
            var mid = (max + min) / 2;
            var firstHalf = await BinarySplit(name, min, mid);
            var secondHalf = await BinarySplit(name, mid + 1, max);
            firstHalf.AddRange(secondHalf);
            return firstHalf;
        }
        return [(min, max)];
    }
    
    /// <summary>
    /// GetByKeyword takes a keyword, an amount and a start cursor and uses these to find the first amount of projects
    /// after the start cursor with the keyword as search phrase. The result includes contributors.
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns>A list of repositories including contributors in the form of List&lt;ProjectDto&gt;</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ProjectDto>> GetByKeyword(string name, int amount, string? startCursor)
    {
        try
        {
            var listResult = await _gitHubGraphqlService.QueryRepositoriesByNameHelper(name, amount, startCursor);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var spiderData in listResult)
            {
                var temp = _graphqlDataConverter.SearchToProjects(spiderData);
                var tasks = temp.Select(async project =>
                {
                    var response = await GetContributorsByName(project.Name, project.Owner, 100);
                    project.Contributors = response;
                });
                await Task.WhenAll(tasks);
                result.AddRange(temp);
            }

            _logger.LogInformation("{Origin}: Returning the project with name: {name}.", this, name);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " in {origin} with request: \"{name}\"", this, name);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }
    }
    
    /// <summary>
    /// GetByTopic takes the name of a topic, an amount and a start cursor and uses those to get the first amount of
    /// repositories with the topic, after the start cursor. The result includes contributors.
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start searching from. If startCursor is null it starts searching from
    /// the start</param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ProjectDto>> GetByTopic(string topic, int amount, string? startCursor)
    {
        try
        {
            var listResult = await _gitHubGraphqlService.QueryRepositoriesByTopicHelper(topic, amount, startCursor);
            List<ProjectDto> result = new List<ProjectDto>();
            foreach (var topicSearchData in listResult)
            {
                if (topicSearchData.Topic == null)
                {
                    continue;
                }
                var temp = _graphqlDataConverter.TopicSearchToProjects(topicSearchData).Where(dto => dto.NumberOfStars >= 5);
                 var tasks = temp.Select(async project =>
                 {
                     var response = await GetContributorsByName(project.Name, project.Owner, 100);
                     project.Contributors = response;
                 });
                await Task.WhenAll(tasks);
                result.AddRange(temp);
            }
            _logger.LogInformation("{Origin}: Returning projects with the topic: {name}.",
                this, topic);
            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message + " in {origin} with request: \"{name}\"", this, topic);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }

    }
    
    /// <summary>
    /// GetByName gets a repository based on it's name and ownerName. The result includes contributors.
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">repository owner name</param>
    /// <returns>A single repository in the form of ProjectDto</returns>
    public async Task<ProjectDto> GetByName(string name, string ownerName)
    {
        var repo = await _gitHubGraphqlService.QueryRepositoryByName(name, ownerName);
        var result = _graphqlDataConverter.RepositoryToProject(repo.Repository);
        result.Contributors = await GetContributorsByName(name, ownerName, 100);
        _logger.LogInformation("{Origin}: Returning repository {name} owned by: {owner}.",
            this, name , ownerName);
        return result;
    }
    
    /// <summary>
    /// GetByNames gets repositories by their name and ownerNames. The result includes contributors.
    /// </summary>
    /// <param name="repos">A list of projectRequestDtos with at least a repoName and ownerName</param>
    /// <returns>Returns the list of requested repositories in the form of List&lt;ProjectDto&gt;</returns>
    public async Task<List<ProjectDto>> GetByNames(List<ProjectRequestDto> repos)
    {
        var listResult = await _gitHubGraphqlService.GetByNames(repos);
        List<ProjectDto> result = new List<ProjectDto>();
        foreach (var spiderData in listResult)
        {
            var temp = _graphqlDataConverter.SearchToProjects(spiderData);
            var tasks = temp.Select(async project =>
            {
                var response = await GetContributorsByName(project.Name, project.Owner, 100);
                project.Contributors = response;
            });
            await Task.WhenAll(tasks);
            result.AddRange(temp);
        }
        _logger.LogInformation("{Origin}: Returning all requested repositories.", this);
        return result;
    }
    
    /// <summary>
    /// Get ContributorsByName gets the contributors of a repository based on the repositories name and ownerName
    /// </summary>
    /// <param name="name">Repository name</param>
    /// <param name="ownerName">Repository owner name</param>
    /// <param name="amount">Amount of contributors to return</param>
    /// <returns>A list of contributors in the form of List&lt;ContributorDto&gt;?. Returns null if there are no
    /// contributors or they cannot be accessed</returns>
    /// <exception cref="HttpRequestException">Throws on NullReferenceException or JsonException</exception>
    public async Task<List<ContributorDto>?> GetContributorsByName(string name, string ownerName, int amount)
    {
        try
        {
            var result = await _gitHubRestService.GetRepoContributors(ownerName,
                name, amount);     
            return result;
        }
        catch (Exception e)
        {
             _logger.LogError(e.Message + " in {origin} with request: \"{name}/{ownerName}\"", this,
                 name, ownerName);
            switch (e)
            {
                case JsonException:
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                }
                case NullReferenceException :
                {
                    var exception = new HttpRequestException("An unexpected error occured", e,
                        HttpStatusCode.InternalServerError);
                    throw exception;
                } 
                    
                default:
                    throw;
            }
        }
    }
}