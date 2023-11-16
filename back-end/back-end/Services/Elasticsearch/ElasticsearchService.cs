﻿using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Analysis;
using n = Nest;

namespace SECODashBackend.Services.ElasticSearch;

public class ElasticsearchService : IElasticsearchService
{
    // Name of the projects index in Elasticsearch
    private const string ProjectIndex = "projects-timed-test";

    // Property of the Project document that contains the topics of the project 
    private const string TopicProperty = "topics.keyword";

    // Used to create and retrieve aggregates in the Elasticsearch queries
    private const string LanguageAggregateName = "languages";
    private const string SumAggregateName = "sum";
    private const string NestedAggregateName = "nested";
    private const string TopicAggregateName = "topics";
    
    private const int NumberOfRequestedProjects = 10000;
    
    private readonly ElasticsearchClient _client;
    public ElasticsearchService(ElasticsearchClient client)
    {
        _client = client;
    }

    public async Task AddProjects(IEnumerable<ProjectDto> projectDtos)
    {
        var request = new BulkRequest(ProjectIndex);
        
        var indexOperations = 
            projectDtos.Select(p =>
            {
                p.Timestamp = DateTime.UtcNow;
                return new BulkIndexOperation<ProjectDto>(p);
                
            });
        request.Operations = new BulkOperationsCollection(indexOperations);
        var response = await _client.BulkAsync(request);
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
    }
    /// <summary>
    /// This method retrieves all projects from the Elasticsearch index that were created between the given dates.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    /// <exception cref="HttpRequestException"></exception>
    public async Task<List<ProjectDto>> GetProjectsByDate(DateTime time)
    {
        // Assuming we want to include projects from the start of the given day
        DateTime timeFrameMin = time.Date;  
        // Assuming we want to include projects until the end of the given day
        DateTime timeFrameMax = timeFrameMin.AddDays(1).AddTicks(-1);  

        var response = await _client.SearchAsync<ProjectDto>(search => search
            .Index(ProjectIndex)
            .From(0)
            .Size(NumberOfRequestedProjects)
            .Query(q => q
                .Range(r => r
                    .DateRange(dr => dr
                        .Field(t => t.Timestamp)
                        .Gte(timeFrameMin)
                        .Lte(timeFrameMax)
                    )
                )
            )
        );
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());

        return response.Documents.ToList();
    }

    public async Task<List<ProjectDto>> GetProjectsByTopic(List<string> topics)
    {
        var response = await _client.SearchAsync<ProjectDto>(search => search
            .Index(ProjectIndex)
            .From(0)
            .Size(NumberOfRequestedProjects)
            .Query(q => q
                .TermsSet(t => t
                    .Field(TopicProperty)
                    .Terms(topics)
                    .MinimumShouldMatchScript(new Script(new InlineScript("params.num_terms"))))));
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());

        return response.Documents.ToList();
    }

    public async Task<EcosystemDto> GetEcosystemData(List<string> topics, int numberOfTopLanguages,
        int numberOfTopSubEcosystems)
    {
       var response = await _client.SearchAsync<ProjectDto>(search => search
            .Index(ProjectIndex)
            .Size(0)
            .Query(q => q
                .TermsSet(t => t
                    .Field(TopicProperty)
                    .Terms(topics)
                    .MinimumShouldMatchScript(new Script(new InlineScript("params.num_terms")))))
            .Aggregations(a => a
                .Topics(TopicAggregateName)
                .SumProgrammingLanguages(NestedAggregateName, LanguageAggregateName, SumAggregateName)
            )
       );
        
        if (!response.IsValidResponse) throw new HttpRequestException(response.ToString());
        
        var nestedAggregate = response.Aggregations?.GetNested(NestedAggregateName);
        var languagesAggregate = nestedAggregate?.GetStringTerms(LanguageAggregateName);
        var topicsAggregate = response.Aggregations?.GetStringTerms(TopicAggregateName);

        var subEcosystems = topicsAggregate?.Buckets
            .Select(topic => new SubEcosystemDto
            {
                Topic = topic.Key.ToString(),
                ProjectCount = (int)topic.DocCount
            }).ToList();

        
        var programmingLanguageDtos = languagesAggregate?.Buckets
            .Select(b => 
                new ProgrammingLanguageDto
                {
                    Language = b.Key.ToString(),
                    Percentage = (float)b.GetSum(SumAggregateName)!.Value!
                })
            .ToList();
        
        return new EcosystemDto
        {
            Topics = topics.ToList(),
            TopLanguages = EcosystemAnalysisService.GetNormalisedTopXLanguages(programmingLanguageDtos!, numberOfTopLanguages),
            SubEcosystems = EcosystemAnalysisService.GetTopXSubEcosystems(subEcosystems!, topics, numberOfTopSubEcosystems) 
        };
    }
}