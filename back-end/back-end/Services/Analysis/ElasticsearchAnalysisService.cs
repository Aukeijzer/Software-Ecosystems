using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.ElasticSearch;

namespace SECODashBackend.Services.Analysis;

/// <summary>
/// Service that analyses an ecosystem by querying the Elasticsearch index for projects that contain the given topics.
/// The service is responsible for retrieving the relevant data from the search response and converting it to the
/// correct format.
/// </summary>
public class ElasticsearchAnalysisService(IElasticsearchService elasticsearchService) : IAnalysisService
{
    // Use the maximum bucket size supported by elasticsearch
    // See https://www.elastic.co/guide/en/elasticsearch/reference/8.11/search-aggregations-bucket.html
    private const int MaxBucketSize = 10000;
    
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
    private const string NestedLanguagesAggregateName = "nested_languages";
    private const string TopicAggregateName = "topics";

    // Dictionary of topics that are programming languages and need to be filtered out
    private static readonly HashSet<string> ProgrammingLanguageTopics = new()
    {
        "c#",
        "c++",
        "java",
        "javascript",
        "python",
        "ruby",
        "rust",
        "typescript",
        "go",
        "php",
        "swift",
        "kotlin",
        "scala",
        "dart",
        "elixir",
        "haskell",
        "r",
        "clojure",
        "erlang",
        "f#",
        "groovy",
        "julia",
        "lua",
        "ocaml",
        "perl",
        "powershell",
        "racket",
        "shell",
        "sql",
        "visual basic",
        "assembly",
        "matlab",
        "objective-c",
        "delphi",
        "cobol",
        "fortran",
        "lisp",
        "pascal",
        "prolog",
        "scheme",
        "smalltalk",
        "abap",
        "apex",
        "coffeescript",
        "crystal"
    };

    /// <summary>
    /// Queries the Elasticsearch index for projects that contain the given topics and analyses the ecosystem.
    /// The analysis consists of two parts:
    /// 1. Retrieving the top x programming languages
    /// 2. Retrieving the top x sub-ecosystems/topics
    /// </summary>
    public async Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors)
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
        var nestedLanguagesAggregation = new NestedAggregation(NestedLanguagesAggregateName)
        {
            Path = LanguagesPropertyPath,
            
            Aggregations = new AggregationDictionary
            {
                // Aggregation of the unique values of the language name field
                // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/terms-set-query-usage.html
                new TermsAggregation(LanguageAggregateName)
                {
                    Field = LanguageNameField,
                    Size = MaxBucketSize,
                    
                    // Aggregation of the sum of the language.percentage field of all languages object with the same name
                    // https://www.elastic.co/guide/en/elasticsearch/client/net-api/7.17/sum-aggregation-usage.html
                    Aggregations = new AggregationDictionary
                    {
                        new SumAggregation(SumAggregateName)
                        {
                            Field = LanguagePercentageField
                        },
                    }
                },
            }
        };
        
        var nestedContributorsAggregation = new NestedAggregation("nested_contributors")
        {
            Path = "contributors",
            Aggregations = new AggregationDictionary
            {
                new TermsAggregation("contributors")
                {
                    Field = "contributors.login.keyword",
                    Size = MaxBucketSize,
                    Aggregations = new AggregationDictionary
                    {
                        new SumAggregation("sum_contributions")
                        {
                            Field = "contributors.contributions"
                        }
                    }
                }
            }
        };

        // Aggregation of all projects aggregated by topic
        var topicAggregation = new TermsAggregation(TopicAggregateName)
        {
            Field = TopicField,
            Size = topics.Count + numberOfTopSubEcosystems + ProgrammingLanguageTopics.Count
        };

        var searchRequest = new SearchRequest
        {
            Query = termsSetQuery,
            Aggregations = new AggregationDictionary
            { 
                nestedLanguagesAggregation,
                nestedContributorsAggregation,
                topicAggregation
            },
            Size = 0 // Do not request actual Project documents
        };
        
        var result = await elasticsearchService.QueryProjects(searchRequest);
        
        return new EcosystemDto
        {
            Topics = topics,
            SubEcosystems = GetTopXSubEcosystems(result, topics, numberOfTopSubEcosystems),
            TopLanguages = GetTopXLanguages(result, numberOfTopLanguages),
            TopContributors = GetTopXContributors(result, numberOfTopContributors)
        };
    }
    
    private static List<TopContributorDto> GetTopXContributors(SearchResponse<ProjectDto> searchResponse, int numberOfTopContributors)
    {
        var nestedAggregate = searchResponse.Aggregations?.GetNested("nested_contributors");
        var contributorsAggregate = nestedAggregate?.GetStringTerms("contributors");

        if (contributorsAggregate == null)
            throw new ArgumentException(
                "Elasticsearch aggregate not found in search response");
        
        var contributorDtos = contributorsAggregate
            .Buckets
            .Select(b => 
                new TopContributorDto
                {
                    Login = b.Key.ToString(),
                    Contributions = (int)b.GetSum("sum_contributions")!.Value!
                })
            .ToList();
        
        var sortedContributors = contributorDtos
            .OrderByDescending(c => c.Contributions);

        var topXContributors = sortedContributors
            .Take(numberOfTopContributors)
            .ToList();
        return topXContributors;
    }
    
    /// <summary>
    /// Retrieves the programming languages from the search response and converts them into a Top x list
    /// </summary>
    private static List<ProgrammingLanguageDto> GetTopXLanguages(
        SearchResponse<ProjectDto> searchResponse, int numberOfTopLanguages)
    {
        var nestedAggregate = searchResponse.Aggregations?.GetNested(NestedLanguagesAggregateName);
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
        List<string> topics, int numberOfTopSubEcosystems)
    {
        var topicsAggregate = searchResponse.Aggregations?.GetStringTerms(TopicAggregateName);
        if(topicsAggregate == null) throw new ArgumentException(
                "Elasticsearch aggregate not found in search response");

        var subEcosystemDtos = topicsAggregate
            .Buckets.Select(topic => new SubEcosystemDto
            {
                Topic = topic.Key.ToString(),
                ProjectCount = (int)topic.DocCount
            });

        var filteredSubEcosystems = FilterSubEcosystems(subEcosystemDtos, topics);
        var sortedSubEcosystems = SortSubEcosystems(filteredSubEcosystems);
        var topXSubEcosystems = sortedSubEcosystems
            .Take(numberOfTopSubEcosystems)
            .ToList();

        return topXSubEcosystems;
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
        var topXLanguages = programmingLanguageDtos
            .Take(numberOfTopLanguages)
            .ToList();
        topXLanguages
            .ForEach(l => l.Percentage = float.Round(l.Percentage / totalSum * 100));
        return topXLanguages;
    }

    /// <summary>
    /// Sorts a list of sub-ecosystems in descending order of the number of projects and returns the sorted list.
    /// </summary>
    public static IEnumerable<SubEcosystemDto> SortSubEcosystems(IEnumerable<SubEcosystemDto> subEcosystemDtos)
    {
        var subEcosystemsList = subEcosystemDtos.ToList();
        subEcosystemsList
            .Sort((x,y) => y.ProjectCount.CompareTo(x.ProjectCount));
        return subEcosystemsList;
    }

    /// <summary>
    ///  Filters out sub-ecosystems that are in the topics list that defines the ecosystem, have fewer than the minimum number of projects
    ///  or are programming languages.
    /// </summary>
    public static IEnumerable<SubEcosystemDto> FilterSubEcosystems(IEnumerable<SubEcosystemDto> subEcosystemDtos, List<string> topics)
    {
        return subEcosystemDtos
            .Where(s => !topics.Contains(s.Topic))
            .Where(s => s.ProjectCount >= MinimumNumberOfProjects)
            .Where(s => !ProgrammingLanguageTopics.Contains(s.Topic));
    }
}