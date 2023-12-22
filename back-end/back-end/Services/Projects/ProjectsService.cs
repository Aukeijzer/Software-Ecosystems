using Elastic.Clients.Elasticsearch;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;
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
    public async Task MineByKeywordAsync(string keyword, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await spiderService.GetProjectsByKeywordAsync(keyword, amount);
        
        // Save these projects to elasticsearch
        await elasticsearchService.AddProjects(newDtos);
    }
    
    /// <summary>
    /// Requests Elasticsearch for projects related to the given time.
    /// </summary>
    /// <param name="time"></param>
    public async Task<int> GetByTimeFrameAsync(DateTime st, DateTime et, string topic)
    {
        var result = await elasticsearchService.GetProjectsByDate(st, et, topic);
        return result;
    }
}