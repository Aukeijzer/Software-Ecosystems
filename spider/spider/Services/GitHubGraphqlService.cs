using System.Text;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using spider.Dtos;
using spider.Models;
using spider.Models.Graphql;

namespace spider.Services;

public class GitHubGraphqlService : IGitHubGraphqlService
{
    private readonly GraphQLHttpClient _client;
    
    public GitHubGraphqlService()
    {
        _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
        var token = Environment.GetEnvironmentVariable("API_Token");
        _client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    }

    public async Task<SpiderData> QueryRepositoriesByName(string repositoryName, int amount = 10)
    {
        // GraphQL query to search the repositories with the given name.
        var repositoriesQuery = new GraphQLHttpRequest()
        {
            Query = @"query repositoriesQueryRequest($name: String!, $_amount : Int!){
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
            Variables = new{name= repositoryName, _amount = amount}
        };
        await _client.SendQueryAsync<Object>(repositoriesQuery);
        
        var response = await _client.SendQueryAsync(repositoriesQuery,  () => new SpiderData());
        return response.Data;
    }
    
    public async Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount)
    {
        var topicRepositoriesQuery = new GraphQLHttpRequest()
        {
            //graphql query to search for repositories based on a github topic
            Query = @"query repositoriesQueryRequest($_topic: String!, $_amount : Int!) {
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
            Variables = new{_topic= topic, _amount = amount}
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