using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.ElasticSearch;

namespace SECODashBackend.Services.Analysis;

public class ElasticsearchAnalysisService : IAnalysisService
{
    // Minimum number of projects in sub-ecosystem for it to end up in the top x list\
    private const int MinimumNumberOfProjects = 2;
    
    // Used to instruct elasticsearch where to find the field/properties in the Project document
    private const string TopicField = "topics.keyword";
    private const string LanguagesPropertyPath = "languages";
    private const string LanguageNameField = "languages.language.keyword";
    private const string LanguagePercentageField = "languages.percentage";

    // Instructs elasticsearch to match using all terms of a term set
    private const string MatchAllParametersScript = "params.num_terms";

    // Used to create and retrieve aggregates in the Elasticsearch queries
    private const string LanguageAggregateName = "languages";
    private const string SumAggregateName = "sum";
    private const string NestedAggregateName = "nested";
    private const string TopicAggregateName = "topics";
    
    private readonly IElasticsearchService _elasticSearchService;

    public ElasticsearchAnalysisService(IElasticsearchService elasticsearchService)
    {
        _elasticSearchService = elasticsearchService;
    }
    
    public async Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems)
    {
        // Query that matches all projects that contain all topics in the topics list
        // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/terms-set-query-usage.html
        var termsSetQuery = new TermsSetQuery(TopicField)
        {
            Terms = topics,
            MinimumShouldMatchScript = new Script(new InlineScript(MatchAllParametersScript)),
        };

        // Aggregation of the nested Language documents in the Project documents
        // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/terms-set-query-usage.html
        var nestedAggregation = new NestedAggregation(NestedAggregateName)
        {
            Path = LanguagesPropertyPath,
            
            Aggregations = new AggregationDictionary
            {
                // Aggregation of the unique values of the language name field
                // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/terms-set-query-usage.html
                new TermsAggregation(LanguageAggregateName)
                {
                    Field = LanguageNameField,
                    
                    // Aggregation of the sum of the language.percentage field of all languages object with the same name
                    // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/sum-aggregation-usage.html
                    Aggregations = new AggregationDictionary
                    {
                        new SumAggregation(SumAggregateName)
                        {
                            Field = LanguagePercentageField,
                        },
                    }
                },

            }
        };

        // Aggregation of all projects aggregated by topic
        var topicAggregation = new TermsAggregation(TopicAggregateName)
        {
            Field = TopicField
        };

        var searchRequest = new SearchRequest
        {
            Query = termsSetQuery,
            Aggregations = new AggregationDictionary
            { 
                nestedAggregation,
                topicAggregation
            },
            Size = 0 // Do not request actual Project documents
        };
        
        var result = await _elasticSearchService.QueryProjects(searchRequest);
        
        return new EcosystemDto
        {
            Topics = topics,
            SubEcosystems = GetTopXSubEcosystems(result, topics, numberOfTopSubEcosystems),
            TopLanguages = GetTopXLanguages(result, numberOfTopLanguages) 
        };
    }
    
    /// <summary>
    /// Retrieves the programming languages from the search response and converts them into a Top x list
    /// </summary>
    private static List<ProgrammingLanguageDto> GetTopXLanguages(
        SearchResponse<ProjectDto> searchResponse, int numberOfTopLanguages)
    {
        var nestedAggregate = searchResponse.Aggregations?.GetNested(NestedAggregateName);
        var languagesAggregate = nestedAggregate?.GetStringTerms(LanguageAggregateName);

        if (languagesAggregate == null)
            throw new ArgumentException(
                "Elasticsearch aggregate not found in search response");
        
        var programmingLanguageDtos = languagesAggregate
            .Buckets
            .Select(b => 
                new ProgrammingLanguageDto
                {
                    Language = b.Key.ToString(),
                    Percentage = (float)b.GetSum(SumAggregateName)!.Value!
                })
            .ToList();

        var topXLanguages = SortAndNormalizeLanguages(programmingLanguageDtos, numberOfTopLanguages);
        return topXLanguages;
    }

    /// <summary>
    /// Retrieves the sub-ecosystems/topics from the search response and converts them into a Top x list
    /// </summary>
    private static List<SubEcosystemDto> GetTopXSubEcosystems(
        SearchResponse<ProjectDto> searchResponse,
        List<string> topics,
        int numberOfTopSubEcosystems)
    {
        var topicsAggregate = searchResponse.Aggregations?.GetStringTerms(TopicAggregateName);
        if(topicsAggregate == null) throw new ArgumentException(
                "Elasticsearch aggregate not found in search response");

        var subEcosystemDtos = topicsAggregate
            .Buckets.Select(topic => new SubEcosystemDto
            {
                Topic = topic.Key.ToString(),
                ProjectCount = (int)topic.DocCount
            }).ToList();

        var topSubEcosystems = SortSubEcosystems(subEcosystemDtos, topics, numberOfTopSubEcosystems);

        return topSubEcosystems;
    }

    /// <summary>
    /// Converts a list of all the programming languages in an ecosystem with the sum of their usage percentages over
    /// all projects to a "Top x" list of x length in descending order of percentage with the percentages normalised.
    /// </summary>
    public static List<ProgrammingLanguageDto> SortAndNormalizeLanguages(
        List<ProgrammingLanguageDto> programmingLanguageDtos, int numberOfTopLanguages)
    {
        programmingLanguageDtos
            .Sort((x, y)  => y.Percentage.CompareTo(x.Percentage));
        var totalSum = programmingLanguageDtos.Sum(l => l.Percentage);
        var topXLanguages = programmingLanguageDtos.Take(numberOfTopLanguages).ToList();
        topXLanguages
            .ForEach(l => l.Percentage = float.Round(l.Percentage / totalSum * 100));
        return topXLanguages;
    }

    /// <summary>
    /// Converts a list of all the sub-ecosystems/topics of an ecosystem into a "Top x" list of x length in descending
    /// order of project count. The topics that define the ecosystem are filtered out.
    /// </summary>
    public static List<SubEcosystemDto> SortSubEcosystems(List<SubEcosystemDto> subEcosystemDtos, List<string> topics,
        int numberOfTopSubEcosystems)
    {
        subEcosystemDtos
            .Sort((x,y) => y.ProjectCount.CompareTo(x.ProjectCount));
        
        var topSubEcosystems = subEcosystemDtos
            .Where(s => !topics.Contains(s.Topic))
            .Take(numberOfTopSubEcosystems)
            .Where(s => s.ProjectCount >= MinimumNumberOfProjects)
            .ToList();
        return topSubEcosystems;
    }
}