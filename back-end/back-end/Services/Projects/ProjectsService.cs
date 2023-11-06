﻿using SECODashBackend.DataConverters;
using SECODashBackend.Models;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Projects;

public class ProjectsService : IProjectsService
{
    private readonly IElasticsearchService _elasticsearchService;
    private readonly ISpiderService _spiderService;

    public ProjectsService(
        IElasticsearchService elasticsearchService,
        ISpiderService spiderService)
    {
        _elasticsearchService = elasticsearchService;
        _spiderService = spiderService;
    }

    // TODO: determine if we still need this functionality, if so, implement it using elasticsearch
    public async Task<Project?> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Project>> GetByTopicsAsync(List<string> topics)
    {
        // Retrieve all related projects from elasticsearch
        var dtos = await _elasticsearchService.GetProjectsByTopic(topics);
        return dtos.Select(ProjectConverter.ToProject);
    }

    public async Task MineByTopicAsync(string topic, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await _spiderService.GetProjectsByTopicAsync(topic, amount);
        
        // Save these projects to elasticsearch
        await _elasticsearchService.AddProjects(newDtos);
    }
    
    public async Task MineByKeywordAsync(string keyword, int amount)
    {
        // Request the Spider for projects related to this topic.
        var newDtos = await _spiderService.GetProjectsByKeywordAsync(keyword, amount);
        
        // Save these projects to elasticsearch
        await _elasticsearchService.AddProjects(newDtos);
    }
}