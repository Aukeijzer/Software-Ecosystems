using Elastic.Clients.Elasticsearch.Aggregations;

namespace SECODashBackend.Services.ElasticSearch;

public static class AggregationDescriptorExtensions
{
    // Extension method for creating an aggregation of all programming languages with their summed percentages over all projects
    public static AggregationDescriptor<TDocument> SumProgrammingLanguages<TDocument>(
        this AggregationDescriptor<TDocument> aggregationDescriptor, string nestedAggregate, string languageAggregate, string sumAggregate)
    {
        return aggregationDescriptor
            .Nested(nestedAggregate, n => n
                .Path("languages")
                .Aggregations(al => al
                    .Terms(languageAggregate, t => t
                            .Field("languages.language.keyword")
                            .Size(10000)
                            .Aggregations(aa => aa
                                .Sum(sumAggregate, sm => sm
                                    .Field("languages.percentage"))
                            )
                    )
                )
            );
    }

    public static AggregationDescriptor<TDocument> Topics<TDocument>(
        this AggregationDescriptor<TDocument> aggregationDescriptor, string topicsAggregateName)
    {
        return aggregationDescriptor.Terms(topicsAggregateName, t => t
            .Field("topics.keyword")
            .Size(10000));
    }
}