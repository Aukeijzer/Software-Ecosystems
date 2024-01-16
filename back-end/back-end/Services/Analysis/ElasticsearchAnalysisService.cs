using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.QueryDsl;
using SECODashBackend.Dtos.Contributors;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Models;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.ElasticSearch;

namespace SECODashBackend.Services.Analysis;

/// <summary>
/// Service that analyses an ecosystem by querying the Elasticsearch index for projects that contain the given topics.
/// The service is responsible for retrieving the relevant data from the search response and converting it to the
/// correct format.
/// </summary>
public class ElasticsearchAnalysisService(IElasticsearchService elasticsearchService, EcosystemsService ecosystemsService) : IAnalysisService
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
    
    // Temporary dictionary of topics that are technologies and need to be filtered out
    /*
    private static readonly HashSet<string> TechnologiesTopics = new()
    {
        "android",
        "ios",
        "macos",
        "windows",
        "linux",
        "Baseband",
        "Bluetooth",
        "Cellular technology",
        "Indoor radio communication",
        "Land mobile radio",
        "Millimeter wave communication",
        "Near field communication",
        "Packet radio networks",
        "Passband",
        "Personal area networks",
        "Radio broadcasting",
        "Radio communication countermeasures",
        "Radio frequency",
        "Radio links",
        "Radio spectrum management",
        "Satellite communication",
        "Satellite ground stations",
        "Software radio",
        "Zigbee"
    };
    */

    /// <summary>
    /// Queries the Elasticsearch index for projects that contain the given topics and analyses the ecosystem.
    /// The analysis consists of two parts:
    /// 1. Retrieving the top x programming languages
    /// 2. Retrieving the top x sub-ecosystems/topics
    /// </summary>
    /// <param name="topics">A list of topics that define the ecosystem.</param>
    /// <param name="numberOfTopLanguages">The number of top programming languages to retrieve.</param>
    /// <param name="numberOfTopSubEcosystems">The number of top sub-ecosystems to retrieve.</param>
    /// <param name="numberOfTopContributors">The number of top contributors to retrieve.</param>
    /// <returns>An EcosystemDto with the top x languages, sub-ecosystems and contributors.</returns>
    public async Task<EcosystemDto> AnalyzeEcosystemAsync(List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems, int numberOfTopContributors, int numberOfTopTechnologies)
    {
        //Set TechnologiesTopics based on the selected ecosystems (topics).
        List<Technology>? TechnologiesTopics = new List<Technology>();
        List<Technology>? temp;
        foreach(string topic in topics)
        {
            temp = await ecosystemsService.GetTechnologiesByNameAsync(topic);
            TechnologiesTopics.AddRange(temp); 
        }
        
        
        
        
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
            Size = topics.Count + numberOfTopSubEcosystems + ProgrammingLanguageTopics.Count + TechnologiesTopics.Count
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
            SubEcosystems = GetTopXSubEcosystems(TechnologiesTopics, result, topics, numberOfTopSubEcosystems),
            TopLanguages = GetTopXLanguages(result, numberOfTopLanguages),
            TopContributors = GetTopXContributors(result, numberOfTopContributors),
            TopTechnologies = GetTopXTechnologies(TechnologiesTopics, result, numberOfTopTechnologies)
        };
    }
    
    /// <summary>
    /// Retrieves the top contributors from the search response and converts them into a Top x list.
    /// The method first gets the nested aggregation for contributors from the search response.
    /// Then, it creates a list of TopContributorDto objects from the buckets of the contributors aggregate.
    /// Each TopContributorDto object contains the login and the total number of contributions of a contributor.
    /// The method then sorts the list of TopContributorDto objects in descending order of contributions.
    /// Finally, it returns the top x contributors from the sorted list.
    /// </summary>
    /// <param name="searchResponse">The search response from Elasticsearch.</param>
    /// <param name="numberOfTopContributors">The number of top contributors to retrieve.</param>
    /// <returns>A list of the top x contributors.</returns>
    private static List<TopContributorDto> GetTopXContributors(SearchResponse<ProjectDto> searchResponse, int numberOfTopContributors)
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
    /// <param name="searchResponse">The search response from Elasticsearch.</param>
    /// <param name="numberOfTopLanguages">The number of top languages to retrieve.</param>
    /// <returns>A list of the top x languages in an ecosystem.</returns>
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
    /// Retrieves the sub-ecosystems/topics from the search response and converts them into a Top x list
    /// </summary>
    /// <param name="searchResponse">The search response from Elasticsearch.</param>
    /// <param name="topics">The list of topics that define the ecosystem.</param>
    /// <param name="numberOfTopSubEcosystems">The number of top sub-ecosystems to retrieve.</param>
    /// <returns>A list of the top x sub-ecosystems in an ecosystem.</returns>
    private static List<SubEcosystemDto> GetTopXSubEcosystems(
        List<Technology>? TechnologiesTopics,
        SearchResponse<ProjectDto> searchResponse,
        List<string> topics, int numberOfTopSubEcosystems)
    {
        var subEcosystemDtos = GetSubEcosystems(searchResponse);

        var filteredSubEcosystems = FilterSubEcosystems(TechnologiesTopics, subEcosystemDtos, topics);
        var sortedSubEcosystems = SortSubEcosystems(filteredSubEcosystems);
        var topXSubEcosystems = sortedSubEcosystems
            .Take(numberOfTopSubEcosystems)
            .ToList();

        return topXSubEcosystems;
    }

    private static List<SubEcosystemDto> GetTopXTechnologies(List<Technology>? TechnologiesTopics, SearchResponse<ProjectDto> searchResponse, int numberOfTopTechnologies)
    {
        var subEcosystemDtos = GetSubEcosystems(searchResponse);
        var filteredTechnologies = FilterTechnologies(TechnologiesTopics, subEcosystemDtos);
        var sortedTechnologies = SortSubEcosystems(filteredTechnologies);
        var topXTechnologies = sortedTechnologies
            .Take(numberOfTopTechnologies)
            .ToList();
        
        return topXTechnologies;
    }

    private static List<SubEcosystemDto> GetSubEcosystems( SearchResponse<ProjectDto> searchResponse)
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
        
        return subEcosystemDtos.ToList();
    }

    /// <summary>
    /// Converts a list of all the programming languages in an ecosystem with the sum of their usage percentages over
    /// all projects to a "Top x" list of x length in descending order of percentage with the percentages normalised.
    /// </summary>
    /// <param name="programmingLanguageDtos">A list of all the programming languages in an ecosystem with the sum of their usage percentages over all projects.</param>
    /// <param name="numberOfTopLanguages">The number of top languages to retrieve.</param>
    /// <returns>A Top x list of the top languages in an ecosystem.</returns>
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
    /// <param name="subEcosystemDtos">A list of sub-ecosystems.</param>
    /// <returns>A list of sub-ecosystems sorted in descending order of the number of projects.</returns>
    public static IEnumerable<SubEcosystemDto> SortSubEcosystems(IEnumerable<SubEcosystemDto> subEcosystemDtos)
    {
        var subEcosystemsList = subEcosystemDtos.ToList();
        subEcosystemsList
            .Sort((x,y) => y.ProjectCount.CompareTo(x.ProjectCount));
        return subEcosystemsList;
    }

    /// <summary>
    ///  Filters out sub-ecosystems that are in the topics list that defines the ecosystem, have fewer than the minimum number of projects,
    ///  are programming languages or are technologies.
    /// </summary>
    /// <param name="subEcosystemDtos">A list of sub-ecosystems.</param>
    /// <param name="topics">A list of topics that define the ecosystem.</param>
    /// <returns>A list of sub-ecosystems filtered by the given topics.</returns>
    public static IEnumerable<SubEcosystemDto> FilterSubEcosystems(List<Technology>? TechnologiesTopics, IEnumerable<SubEcosystemDto> subEcosystemDtos, List<string> topics)
    {
        List<string>? technologiesTopics = TechnologiesTopics.ConvertAll(x => x.ToString());
       return subEcosystemDtos
            .Where(s => !topics.Contains(s.Topic))
            .Where(s => s.ProjectCount >= MinimumNumberOfProjects)
            .Where(s => !ProgrammingLanguageTopics.Contains(s.Topic))
            .Where(s => !technologiesTopics.Contains(s.Topic));
    }
    
    public static IEnumerable<SubEcosystemDto> FilterTechnologies(List<Technology>? TechnologiesTopics, IEnumerable<SubEcosystemDto> subEcosystemDtos)
    {
        List<string>? technologiesTopics = TechnologiesTopics.ConvertAll(x => x.ToString());
        return subEcosystemDtos
            .Where(s => technologiesTopics.Contains(s.Topic));
    }
}