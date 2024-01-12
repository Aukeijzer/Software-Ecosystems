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
    #region Constants
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
    private const string ContributorsPath = "contributors";
    private const string ContributorLoginField = "contributors.login.keyword";
    private const string ContributorContributionsField = "contributors.contributions";

    // Instructs elasticsearch to match using all terms of a term set
    private const string MatchAllParametersScript = "params.num_terms";

    // Used to create and retrieve aggregates in the Elasticsearch queries
    private const string LanguageAggregateName = "languages";
    private const string PercentageSumAggregateName = "sum_percentage";
    private const string NestedLanguagesAggregateName = "nested_languages";
    private const string TopicAggregateName = "topics";
    
    private const string NestedContributorsAggregateName = "nested_contributors";
    private const string TermsContributorsAggregateName = "contributors";
    private const string ContributionsSumAggregateName = "sum_contributions";

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
    
    #endregion
    
    /// <summary>
    /// Queries the Elasticsearch index for projects that contain the given topics and analyses the ecosystem.
    /// The analysis consists of two parts:
    /// 1. Retrieving the top x programming languages
    /// 2. Retrieving the top x sub-ecosystems/topics
    /// </summary>
    public async Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors, DateTime startTime, DateTime endTime, int timeBucket)
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
                        new SumAggregation(PercentageSumAggregateName)
                        {
                            Field = LanguagePercentageField
                        },
                    }
                },
            }
        };
        
        // Aggregation of the nested Contributor documents in the Project documents 
        var nestedContributorsAggregation = new NestedAggregation(NestedContributorsAggregateName)
        {
            Path = ContributorsPath,
            Aggregations = new AggregationDictionary
            {
                // Aggregation of the unique values of the contributor.login field
                new TermsAggregation(TermsContributorsAggregateName)
                {
                    Field = ContributorLoginField,
                    Size = MaxBucketSize,
                    
                    // Aggregation of the projects that contain the contributor
                    Aggregations = new AggregationDictionary
                    {
                        // Aggregation of the sum of the contributor.contributions field of all contributors objects with the same login
                        new SumAggregation(ContributionsSumAggregateName)
                        {
                            Field = ContributorContributionsField
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
                topicAggregation,
                nestedLanguagesAggregation,
                nestedContributorsAggregation,
            },
            Size = 0 // Do not request actual Project documents
        };
        
        var result = await elasticsearchService.QueryProjects(searchRequest);
        var contributors = GetAllContributors(result);
        var filteredSubEcosystems = GetAllSubEcosystems(result, topics);
        var topXSubEcosystems = GetTopXSubEcosystems(filteredSubEcosystems, numberOfTopSubEcosystems);
        
        var ecoResponse = new EcosystemDto
        {
            Topics = topics,
            TopSubEcosystems = topXSubEcosystems,
            TopLanguages = GetTopXLanguages(result, numberOfTopLanguages),
            TopContributors = GetTopXContributors(numberOfTopContributors, contributors),
            NumberOfTopics = filteredSubEcosystems.Count,
            NumberOfProjects = result.Total,
            NumberOfContributors = contributors.Count,
            NumberOfContributions = contributors.Sum(c => c.Contributions),
            TimedDataTopics = await GetTimedData(startTime, endTime, elasticsearchService, timeBucket, topXSubEcosystems.Select(s => s.Topic).ToList()),
            TimedDataEcosystem = await GetTimedData(startTime, endTime, elasticsearchService, timeBucket, topics)
        };
        
        return ecoResponse;
    }
 
    #region Contributors
    /// <summary>
    /// Retrieves the top contributors from the search response and converts them into a Top x list.
    /// The method first gets the nested aggregation for contributors from the search response.
    /// Then, it creates a list of TopContributorDto objects from the buckets of the contributors aggregate.
    /// Each TopContributorDto object contains the login and the total number of contributions of a contributor.
    /// The method then sorts the list of TopContributorDto objects in descending order of contributions.
    /// Finally, it returns the top x contributors from the sorted list.
    /// </summary>
    private static List<TopContributorDto> GetTopXContributors(int numberOfTopContributors, List<TopContributorDto> contributorDtos)
    {
        var sortedContributors = contributorDtos
            .OrderByDescending(c => c.Contributions);

        var topXContributors = sortedContributors
            .Take(numberOfTopContributors)
            .ToList();
        return topXContributors;
    }
    
    /// <summary>
    /// This method retrieves all contributors from the search response and converts them into a list of TopContributorDto objects.
    /// </summary>
    private static List<TopContributorDto> GetAllContributors(SearchResponse<ProjectDto> searchResponse)
    {
        var nestedAggregate = searchResponse.Aggregations?.GetNested(NestedContributorsAggregateName);
        var contributorsAggregate = nestedAggregate?.GetStringTerms(TermsContributorsAggregateName);

        if (contributorsAggregate == null)
            throw new ArgumentException(
                "Elasticsearch aggregate not found in search response");
        
        var contributorDtos = contributorsAggregate
            .Buckets
            .Select(b => 
                new TopContributorDto
                {
                    Login = b.Key.ToString(),
                    Contributions = (int)b.GetSum(ContributionsSumAggregateName)!.Value!
                })
            .ToList();
        
        return contributorDtos;
    }
    
    #endregion
    
    #region SubEcosystems
    /// <summary>
    /// Retrieves the sub-ecosystems/topics from the search response and converts them into a Top x list
    /// </summary>
    private static List<SubEcosystemDto> GetTopXSubEcosystems(List<SubEcosystemDto> filteredSubEcosystems, int numberOfTopSubEcosystems)
    {
        var sortedSubEcosystems = SortSubEcosystems(filteredSubEcosystems);
        var topXSubEcosystems = sortedSubEcosystems
            .Take(numberOfTopSubEcosystems)
            .ToList();

        return topXSubEcosystems;
    }
    
    /// <summary>
    /// This method retrieves all sub-ecosystems/topics from the search response and converts them into a list of SubEcosystemDto objects.
    /// It also filters out sub-ecosystems that are in the topics list that defines the ecosystem,
    /// have fewer than the minimum number of projects and are programming languages.
    /// </summary>
    private static List<SubEcosystemDto> GetAllSubEcosystems(SearchResponse<ProjectDto> searchResponse, List<string> topics)
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
        return filteredSubEcosystems.ToList();
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
    
    #endregion
    
    #region Languages
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
                    Percentage = (float)b.GetSum(PercentageSumAggregateName)!.Value!
                })
            .ToList();

        var topXLanguages = SortAndNormalizeLanguages(programmingLanguageDtos, numberOfTopLanguages);
        return topXLanguages;
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
    
    #endregion
    
    #region TimedData
    /// <summary>
    /// This method retrieves the timed data for a specific time frame.
    /// For every topic in the sub-ecosystems/topics list, it retrieves the number of projects that contain that topic.
    /// Then it creates a TimedDateDto object with the topic, the time bucket and the number of projects.
    /// Lastly, it adds the TimedDateDto object to the response list.
    /// </summary>
    private static async Task<List<TimedDataDto>> GetTimedData(DateTime startTime, DateTime endTime, 
        IElasticsearchService elasticsearchService, int timeBucket, List<string> topXTopics)
    {
        var response = new List<TimedDataDto>();
        
        while (startTime < endTime)
        {
            var tasks = new List<Task<TimedDataDto>>();
            foreach(var topic in topXTopics)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var timedTopics = await elasticsearchService.GetProjectsByDate(startTime, endTime, topic);
                    return new TimedDataDto()
                    {
                        Topic = topic,
                        TimeBucket = startTime.ToString("MM-yyyy"),
                        ProjectCount = timedTopics
                    };
                }));
            }
            
            var timedDates = await Task.WhenAll(tasks);
            response.AddRange(timedDates);
            
            startTime = startTime.AddDays(timeBucket);
        }
        
        
        return response;
    }
    
    
    #endregion
}