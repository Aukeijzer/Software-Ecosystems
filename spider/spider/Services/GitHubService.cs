using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using spider.Models;

namespace spider.Services;

public class GitHubService : IGitHubService
{
    private readonly GraphQLHttpClient _client;
    
    public GitHubService()
    {
        _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
        var Token = Environment.GetEnvironmentVariable("API_Token");
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
    }

    private static async void RateLimitCheck(GraphQLHttpClient client)
    {
        var RateLimitCheck = new GraphQLHttpRequest()
        {
            Query = @"query rateLimit", OperationName = "RateLimitCheck"
        };
        var response = await client.SendQueryAsync<string>(RateLimitCheck);
        Console.WriteLine(response.Data);
    }

    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName)
    {
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $fileName : String!){
                        search(query: $name, type: REPOSITORY, first: 10) {
                        repositoryCount
                         nodes {
                            ... on Repository {
                                name
                                owner{
                                    login
                                }
                                repositoryTopics(first: 10){
                                    nodes{
                                        topic{
                                        name
                                        }
                                    }
                                }
                                object(expression: $fileName){
                                    ... on Blob{
                                        text
                                        }
                                    }
                                }
                            }
                        }
                    }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{name= repositoryName, fileName = "main:README.md"}
        };
        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());
        return response.Data;
    }
}