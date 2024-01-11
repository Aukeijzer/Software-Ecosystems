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
    /// Requests the Spider for projects related to the given taxonomy and saves them to Elasticsearch.
    /// </summary>
    public async Task MineByTaxonomy(List<string> taxonomy, int keywordAmount, int topicAmount)
    {
        ConcurrentDictionary<string,ProjectDto> newDtos = new ConcurrentDictionary<string, ProjectDto>();
        // Request the Spider for projects related to each of the terms in the taxonomy.
        foreach (var term in taxonomy)
        {
            var newKeywordDtos = await spiderService.GetProjectsByKeywordAsync(term, keywordAmount);
            foreach (var newKeywordDto in newKeywordDtos)
            {
                newDtos.TryAdd(newKeywordDto.Id, newKeywordDto);
            }
            var newTopicDtos = await spiderService.GetProjectsByTopicAsync(term, topicAmount);
            foreach (var newTopicDto in newTopicDtos)
            {
                newDtos.TryAdd(newTopicDto.Id, newTopicDto);
            }
        }
        await elasticsearchService.AddProjects(newDtos.Values.ToList());
    }
}