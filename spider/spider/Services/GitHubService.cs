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

    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName, int amount)
    {
        // GraphQL query to search the respositories with the given name.
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $fileName : String!, $_amount : Int!){
                        search(query: $name, type: REPOSITORY, first: $_amount) {
                        repositoryCount
                         nodes {
                            ... on Repository {
                                name
                                id
                                description
                                stargazerCount
                                languages(first: 100) {
                                  totalSize
                                  edges {
                                    size
                                    node {
                                      name
                                    }
                                  }
                                }
                                owner{
                                    login
                                }
                                repositoryTopics(first: $_amount){
                                    nodes{
                                        topic{
                                        name
                                        }
                                    }
                                }
                                readme: object(expression: $fileName){
                                    ... on Blob{
                                        text
                                        }
                                    }
                                }
                            }
                        }
                    }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{name= repositoryName, fileName = "main:README.md", _amount = amount}
        };
        var responseString = await _client.SendQueryAsync<Object>(repositoriesQuery);
        
        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());
        return response.Data;
    }
    
    public async Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount = 10)
    {
        var topicRepositoriesQuery = new GraphQLHttpRequest()
        {
            //graphql guery to search for repositories based on a github topic
            Query = @"query repositoriesQueryRequest($_topic: String!, $fileName : String!, $_amount : Int!) {
                        topic(name: $_topic) {
                            repositories(first: $_amount) {
                                nodes {
                                    name
                                    id
                                    description
                                    stargazerCount
                                    languages(first: 100) {
                                      totalSize
                                      edges {
                                        size
                                        node {
                                          name
                                        }
                                      }
                                    }
                                    owner{
                                        login
                                    }
                                    repositoryTopics(first: $_amount){
                                        nodes{
                                            topic{
                                            name
                                            }
                                        }
                                    }
                                    readme: object(expression: $fileName){
                                        ... on Blob{
                                            text
                                            }
                                        }
                                    }
                                  }
                             }
                           }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{_topic= topic, fileName = "main:README.md", _amount = amount}
        };
        
        var response = await _client.SendQueryAsync(topicRepositoriesQuery,  () => new TopicSearchData());
        return response.Data;
    }
}