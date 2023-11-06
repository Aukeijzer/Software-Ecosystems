using System.Net;
using System.Text;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Mvc;
using spider.Dtos;
using spider.Models.Graphql;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace spider.Services;

public class GitHubGraphqlService : IGitHubGraphqlService
{
    private readonly GraphQLHttpClient _client;
    private readonly ILogger<GitHubGraphqlService> _logger;
    
    public GitHubGraphqlService(ILogger<GitHubGraphqlService> logger)
    {
        _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
        var token = Environment.GetEnvironmentVariable("API_Token");
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        _client.HttpClient.DefaultRequestHeaders.Add("X-Github-Next-Global-ID", "1");
        _logger = logger;
    }

    public async Task<List<SpiderData>> QueryRepositoriesByNameHelper(String name, int amount, string? startCursor)
    {
        var projects = new List<SpiderData>();
        string? cursor = startCursor;
      
        while (amount > 0)
        {
            if (amount > 25)
            {
                var temp = await QueryRepositoriesByName(name, 25, cursor);
                amount -= 25;
                if (temp == null)
                {
                    break; 
                }
                projects.Add(temp);
                if (temp.Search.PageInfo?.HasNextPage != true)
                {
                    break;
                }
                cursor = temp.Search.PageInfo?.EndCursor;
            }
            else
            {
                projects.Add(await QueryRepositoriesByName(name, amount, cursor));
                amount = 0;
            }
        }
        return projects;
    }

    
    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName, int amount = 10, string? cursor = null, int tries = 3)
    {
        // GraphQL query to search the repositories with the given name.
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $_amount : Int!, $_cursor : String){
                        search(query: $name, type: REPOSITORY, first: $_amount, after: $_cursor) {
                        repositoryCount
                        pageInfo{
                          startCursor
                          endCursor
                          hasNextPage
                        }
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
                                readmeCaps: object(expression: ""HEAD:README.md"") {
                                  ... on Blob {
                                    text
                                  }
                                }
                                readmeLower: object(expression: ""HEAD:readme.md"") {
                                  ... on Blob {
                                    text
                                  }
                                }
                                readmeFstCaps: object(expression: ""HEAD:Readme.md"") {
                                  ... on Blob {
                                    text
                                  }
                                }
                                readmerstCaps: object(expression: ""HEAD:README.rst"") {
                                  ... on Blob {
                                    text
                                  }
                                }
                                readmerstLower: object(expression: ""HEAD:readme.rst"") {
                                  ... on Blob {
                                    text
                                  }
                                }
                                readmerstFstCaps: object(expression: ""HEAD:Readme.rst"") {
                                  ... on Blob {
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
            Variables = new{name= repositoryName, _amount = amount, _cursor = cursor}
        };
        
        try
        {
            var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());

            if (response is GraphQLHttpResponse<SpiderData> httpResponse && httpResponse.Errors == null)
            {
                return response.Data;
            }

            foreach (var error in response.Errors)
            {
                _logger.LogError("{origin}.QueryRepositoriesByName with request: \"{repositoryName}\" " +
                                 "has failed with graphql error" + error.Message, this,
                    repositoryName);
            }

            return response.Data;
        }
        catch (Exception e)
        {
            switch (e)
            {
                case GraphQLHttpRequestException error:
                    _logger.LogError(e.Message + " in {origin} with request: \"{repositoryName}\"", this, repositoryName);
                    if (error.StatusCode == HttpStatusCode.BadGateway)
                    {
                        if (tries > 1)
                        {
                            return await QueryRepositoriesByName(repositoryName, amount, cursor, tries - 1);
                        }

                        throw new BadHttpRequestException(e.Message);
                    }
                    break;
                default:
                    _logger.LogError(e.Message + " in {origin} with request: \"{repositoryName}\"", this, repositoryName);
                    break;
            }
            throw e;
        }
    }
    
    public async Task<List<TopicSearchData>> QueryRepositoriesByTopicHelper(String topic, int amount, string? startCursor)
    {
        var projects = new List<TopicSearchData>();
        string? cursor = startCursor;
      
        while (amount > 0)
        {
            if (amount > 25)
            {
                var temp = await QueryRepositoriesByTopic(topic, 25, cursor);
                amount -= 25;
                projects.Add(temp);
                cursor = temp.Topic?.Repositories.PageInfo?.EndCursor;
            }
            else
            {
                var temp = await QueryRepositoriesByTopic(topic, amount, cursor);
                projects.Add(temp);
                amount = 0;
            }
        }
        return projects;
    }
    
    public async Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount, string? cursor = null)
    {
        var topicRepositoriesQuery = new GraphQLHttpRequest()
        {
            //graphql query to search for repositories based on a github topic
            Query = @"query repositoriesQueryRequest($_topic: String!, $_amount: Int!, $_cursor: String) {
                        topic(name: $_topic) {
                          repositories(first: $_amount, after: $_cursor, orderBy: {field: STARGAZERS, direction: DESC}) {
                            pageInfo {
                              startCursor
                              endCursor
                              hasNextPage
                            }
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
                              readmeCaps: object(expression: ""HEAD:README.md"") {
                                ... on Blob {
                                  text
                                }
                              }
                              readmeLower: object(expression: ""HEAD:readme.md"") {
                                ... on Blob {
                                  text
                                }
                              }
                              readmeFstCaps: object(expression: ""HEAD:Readme.md"") {
                                ... on Blob {
                                  text
                                }
                              }
                              readmerstCaps: object(expression: ""HEAD:README.rs"") {
                                ... on Blob {
                                  text
                                }
                              }
                              readmerstLower: object(expression: ""HEAD:readme.rs"") {
                                ... on Blob {
                                  text
                                }
                              }
                              readmerstFstCaps: object(expression: ""HEAD:Readme.rst"") {
                                ... on Blob {
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
            Variables = new{_topic= topic, _amount = amount, _cursor = cursor}
        };
        
        var response = await _client.SendQueryAsync(topicRepositoriesQuery,
            () => new TopicSearchData());
        return response.Data;
    }
    
    public async Task<RepositoryWrapper> QueryRepositoryByName(string repositoryName, string ownerName)
    {
        // GraphQL query to search a repository with the given repository name and owner name.
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $_ownerName: String!) {
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
                            readmeCaps: object(expression: ""HEAD:README.md"") {
                               ... on Blob {
                                 text
                               }
                             }
                             readmeLower: object(expression: ""HEAD:readme.md"") {
                               ... on Blob {
                                 text
                               }
                             }
                             readmeFstCaps: object(expression: ""HEAD:Readme.md"") {
                               ... on Blob {
                                 text
                               }
                             }
                             readmerstCaps: object(expression: ""HEAD:README.rst"") {
                               ... on Blob {
                                 text
                               }
                             }
                             readmerstLower: object(expression: ""HEAD:readme.rst"") {
                               ... on Blob {
                                 text
                               }
                             }
                             readmerstFstCaps: object(expression: ""HEAD:Readme.rst"") {
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
            Variables = new{name= repositoryName, _ownerName = ownerName}
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