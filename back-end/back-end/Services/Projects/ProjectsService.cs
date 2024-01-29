using System.Collections.Concurrent;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;
using SECODashBackend.Services.DataProcessor;

namespace SECODashBackend.Services.Projects;

/// <summary>
/// This service is responsible for requesting the Spider for projects, requesting the Data Processor for additional
/// topics and saving them to Elasticsearch.
/// </summary>
public class ProjectsService(IElasticsearchService elasticsearchService,
        ISpiderService spiderService, IDataProcessorService dataProcessorService)
    : IProjectsService
{
    /// <summary>
    /// Requests the Spider for projects related to the given topic, requests Data Processor for additional topics
    /// and saves them to Elasticsearch.
    /// </summary>
    /// <param name="topic">The topic to to search for. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task MineByTopicAsync(string topic, string ecosystem, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await spiderService.GetProjectsByTopicAsync(topic, amount);

        // Request the data processor for additional topics 
        var topicDtos = await dataProcessorService.GetTopics(newDtos);
        
        foreach (var dto in newDtos)
        {
            if (!dto.Topics.Contains(ecosystem))
            {
                dto.Topics.Add(ecosystem);
            }
        }
        // Save these projects to elasticsearch
        await elasticsearchService.AddProjects(topicDtos);
    }
    /// <summary>
    /// Requests the Spider for projects related to the given keyword, requests Data Processor for additional topics
    /// and saves them to Elasticsearch.
    /// </summary>
    /// <param name="keyword">The keyword to to search for. </param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="amount">The amount of repos to search for. </param>
    public async Task MineByKeywordAsync(string keyword, string ecosystem, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await spiderService.GetProjectsByKeywordAsync(keyword, amount);
        
        foreach (var dto in newDtos)
        {
            if (!dto.Topics.Contains(ecosystem))
            {
                dto.Topics.Add(ecosystem);
            }
        }
        // Request the data processor for additional topics 
        var topicDtos = await dataProcessorService.GetTopics(newDtos);
        
        // Save these projects to elasticsearch
        await elasticsearchService.AddProjects(topicDtos);
    }

    /// <summary>
    /// Requests the Spider for projects related to the given taxonomy, requests Data Processor for additional topics
    /// and saves them to Elasticsearch.
    /// </summary>
    /// <param name="taxonomy">The list of strings to mine off of github</param>
    /// <param name="ecosystem">The ecosystem the request is linked to</param>
    /// <param name="keywordAmount">The amount of repos to search for with keyword search</param>
    /// <param name="topicAmount">The amount of repos to search for with topic search</param>
    public async Task MineByTaxonomyAsync(List<string> taxonomy, string ecosystem, int keywordAmount, int topicAmount)
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
                    if (!newKeywordDto.Topics.Contains(ecosystem))
                    {
                        newKeywordDto.Topics.Add(ecosystem);
                    }
                    newDtos.TryAdd(newKeywordDto.Id, newKeywordDto);
                }
            }));
            
            tasks.Add(Task.Run(async () =>
            {
                var newTopicDtos = await spiderService.GetProjectsByTopicAsync(term, topicAmount);
                foreach (var newTopicDto in newTopicDtos)
                {
                    if (!newTopicDto.Topics.Contains(ecosystem))
                    {
                        newTopicDto.Topics.Add(ecosystem);
                    }
                    newDtos.TryAdd(newTopicDto.Id, newTopicDto);
                }
            }));
            
        }
        await Task.WhenAll(tasks);
        
        // Convert to list of projectDtos 
        var projectDtos = newDtos.Values.ToList();
        
        // Request the data processor for additional topics 
        var topicDtos = await dataProcessorService.GetTopics(projectDtos);
        
        await elasticsearchService.AddProjects(topicDtos);
    }
}