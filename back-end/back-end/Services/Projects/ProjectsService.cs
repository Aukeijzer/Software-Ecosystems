using Elastic.Clients.Elasticsearch;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;
using System.Collections.Concurrent;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Projects;

/// <summary>
/// This service is responsible for requesting the Spider for projects and saving them to Elasticsearch.
/// </summary>
public class ProjectsService(IElasticsearchService elasticsearchService,
        ISpiderService spiderService)
    : IProjectsService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic and saves them to Elasticsearch.
    /// </summary>
    /// <param name="topic">The topic to to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task MineByTopicAsync(string topic, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await spiderService.GetProjectsByTopicAsync(topic, amount);
        
        // Save these projects to elasticsearch
        await elasticsearchService.AddProjects(newDtos);
    }
    /// <summary>
    /// Requests the Spider for projects related to the given keyword and saves them to Elasticsearch.
    /// </summary>
    /// <param name="keyword">The keyword to to search for. </param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task MineByKeywordAsync(string keyword, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await spiderService.GetProjectsByKeywordAsync(keyword, amount);
        
        // Save these projects to elasticsearch
        await elasticsearchService.AddProjects(newDtos);
    }
    
    /// <summary>
    /// Returns a project count of the projects that were created in the given DateRange and contain the given topic.
    /// </summary>
    public async Task<long> GetByTimeFrameAsync(DateTime startTime, DateTime endTime, string topic)
    {
        var result = await elasticsearchService.GetProjectsByDate(startTime, endTime, topic);
        return result;
    }
    
    /// <summary>
    /// Requests the Spider for projects related to the given taxonomy and saves them to Elasticsearch.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    public async Task MineByTaxonomyAsync(List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        ConcurrentDictionary<string,ProjectDto> newDtos = new ConcurrentDictionary<string, ProjectDto>();
        // Request the Spider for projects related to each of the terms in the taxonomy.
        var tasks = new List<Task>();
        foreach (var term in taxonomy)
        {
            tasks.Add(Task.Run(async () => 
            {
                var newKeywordDtos = await spiderService.GetProjectsByKeywordAsync(term, keywordAmount);
                foreach (var newKeywordDto in newKeywordDtos)
                {
                    newDtos.TryAdd(newKeywordDto.Id, newKeywordDto);
                }
            }));
            
            tasks.Add(Task.Run(async () =>
            {
                var newTopicDtos = await spiderService.GetProjectsByTopicAsync(term, topicAmount);
                foreach (var newTopicDto in newTopicDtos)
                {
                    newDtos.TryAdd(newTopicDto.Id, newTopicDto);
                }
            }));
            
        }
        await Task.WhenAll(tasks);
        await elasticsearchService.AddProjects(newDtos.Values.ToList());
    }
}