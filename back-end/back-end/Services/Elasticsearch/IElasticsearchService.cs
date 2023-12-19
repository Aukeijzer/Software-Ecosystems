﻿using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.Project;


namespace SECODashBackend.Services.ElasticSearch;

/// <summary>
/// Interface for the service that is responsible for handling all Elasticsearch related requests. 
/// </summary>
public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   public Task<List<ProjectDto>> GetProjectsByDate(DateTime st, DateTime et, List<string> topic);
   public Task<SearchResponse<ProjectDto>> QueryProjects(SearchRequest searchRequest);
}