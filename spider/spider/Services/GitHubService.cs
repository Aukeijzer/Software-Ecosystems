﻿using System.Text;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using spider.Dtos;
using spider.Models;

namespace spider.Services;

public class GitHubService : IGitHubService
{
    private readonly GraphQLHttpClient _client;
    
    public GitHubService()
    {
        _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
        var token = Environment.GetEnvironmentVariable("API_Token");
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }

    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName, int amount = 10, string readmeName = "main:README.md")
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
                                defaultBranchRef {
                                  name
                                  target {
                                    ... on Commit {
                                      history(first: 1) {
                                        edges {
                                          node {
                                            ... on Commit {
                                              committedDate
                                            }
                                          }
                                        }
                                      }
                                    }
                                  }
                                }
                                createdAt
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
                                repositoryTopics(first: 100){
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
                        rateLimit {
                          remaining
                          cost
                          limit
                          resetAt
                          used
                        }
                    }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{name= repositoryName, fileName = readmeName, _amount = amount}
        };
        await _client.SendQueryAsync<Object>(repositoriesQuery);
        
        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());
        return response.Data;
    }
    
    public async Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount, string readmeName)
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
                                    defaultBranchRef {
                                      name
                                      target {
                                        ... on Commit {
                                          history(first: 1) {
                                            edges {
                                              node {
                                                ... on Commit {
                                                  committedDate
                                                }
                                              }
                                            }
                                          }
                                        }
                                      }
                                    }
                                    createdAt
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
                                    repositoryTopics(first: 100){
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
                                    rateLimit {
                                      remaining
                                      cost
                                      limit
                                      resetAt
                                      used
                                    }
                                  }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{_topic= topic, fileName = readmeName, _amount = amount}
        };
        
        var response = await _client.SendQueryAsync(topicRepositoriesQuery,  () => new TopicSearchData());
        return response.Data;
    }
    
    public async Task<RepositoryWrapper> QueryRepositoryByName(string repositoryName, string ownerName, string readmeName)
    {
        // GraphQL query to search a repository with the given repository name and owner name.
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $fileName: String!, $_ownerName: String!) {
                        repository(name: $name, owner: $_ownerName) {
                            name
                            id
                            defaultBranchRef {
                              name
                              target {
                                ... on Commit {
                                  history(first: 1) {
                                    edges {
                                      node {
                                        ... on Commit {
                                          committedDate
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }
                            createdAt
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
                            owner {
                              login
                            }
                            repositoryTopics(first: 100) {
                              nodes {
                                topic {
                                  name
                                }
                              }
                            }
                            readme: object(expression: $fileName) {
                              ... on Blob {
                                text
                              }
                            }
                          }
                          rateLimit {
                            remaining
                            cost
                            limit
                            resetAt
                            used
                          }
                        }",
            OperationName = "repositoriesQueryRequest",
            Variables = new{name= repositoryName, _ownerName = ownerName,fileName = readmeName}
        };

        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new RepositoryWrapper());
        return response.Data;
    }
    
    public async Task<SpiderData> ToQueryString(List<ProjectRequestDto> repos)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var repo in repos)
        {
            stringBuilder.Append("repo:");
            stringBuilder.Append(repo.OwnerName);
            stringBuilder.Append('/');
            stringBuilder.Append(repo.RepoName);
            stringBuilder.Append(' ');
        }

        string query = stringBuilder.ToString();
        
       return (await QueryRepositoriesByName(query, repos.Count));
    }
}