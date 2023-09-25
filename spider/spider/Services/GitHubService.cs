﻿using GraphQL.Client.Abstractions;
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

    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName)
    {
        // GraphQL query to search the respositories with the given name.
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
            Variables = new{name= repositoryName, fileName = "main:README.md"}
        };
        //var responseString = await _client.SendQueryAsync<Object>(repositoriesQuery);
        
        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());
        return response.Data;
    }
}