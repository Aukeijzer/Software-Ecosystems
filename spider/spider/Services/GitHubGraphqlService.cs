using System.Net;
using System.Text;
using GraphQL.Client.Http;
using spider.Dtos;
using spider.Models.Graphql;
using spider.Wrappers;
using BadHttpRequestException = Microsoft.AspNetCore.Http.BadHttpRequestException;

namespace spider.Services;

public class GitHubGraphqlService : IGitHubGraphqlService
{
    private readonly IClientWrapper _client;
    private readonly ILogger<GitHubGraphqlService>? _logger;
    
    public GitHubGraphqlService(IClientWrapper clientWrapper)
    {
      _client = clientWrapper;
      _logger = new Logger<GitHubGraphqlService>(new LoggerFactory());
    }
  
    /// <summary>
    /// QueryRepositoriesByNameHelper splits the incoming request into smaller parts
    /// </summary>
    /// <param name="name">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from</param>
    /// <returns>list of repositories in the form of List&lt;SpiderData&gt;</returns>
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

    
    /// <summary>
    /// QueryRepositoriesByName sends a graphql request to the github api and returns on success and otherwise handles
    /// the error and retries if necessary.
    /// </summary>
    /// <param name="repositoryName">Keyword to search by</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="cursor">The cursor to start the search from</param>
    /// <param name="tries">amount of retries before failing</param>
    /// <returns>list of repositories in the form of SpiderData</returns>
    /// <exception cref="BadHttpRequestException">If it fails after tries amount of retries throw</exception>
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
            var response = await _client.SendQueryAsync<SpiderData>(repositoriesQuery);

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
            throw; 
        }
    }
    
    /// <summary>
    /// QueryRepositoriesByTopicHelper splits the incoming request into smaller parts
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="startCursor">The cursor to start the search from</param>
    /// <returns>list of repositories in the form of List&lt;TopicSearchData&gt;</returns>
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
                if (temp.Topic.Repositories.PageInfo?.HasNextPage != true)
                {
                  break;
                }
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
    
    /// <summary>
    /// QueryRepositoriesByTopic sends a graphql request to the github api and returns on success and otherwise handles
    /// the error and retries if necessary.
    /// </summary>
    /// <param name="topic">topic to search for</param>
    /// <param name="amount">Amount of repositories to return</param>
    /// <param name="cursor">The cursor to start the search from</param>
    /// <param name="tries">amount of retries before failing</param>
    /// <returns>list of repositories in the form of TopicSearchData</returns>
    /// <exception cref="BadHttpRequestException">If it fails after tries amount of retries throw</exception>
    public async Task<TopicSearchData> QueryRepositoriesByTopic(string topic, int amount, string? cursor = null, int tries = 3)
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
        
        
        try
        {
          var response = await _client.SendQueryAsync<TopicSearchData>(topicRepositoriesQuery);

          if (response is GraphQLHttpResponse<TopicSearchData> httpResponse && httpResponse.Errors == null)
          {
            return response.Data;
          }

          foreach (var error in response.Errors)
          {
            _logger.LogError("{origin}.QueryRepositoriesByTopic with request: \"{topic}\" " +
                             "has failed with graphql error" + error.Message, this,
              topic);
          }

          return response.Data;
        }
        catch (Exception e)
        {
          switch (e)
          {
            case GraphQLHttpRequestException error:
              _logger.LogError(e.Message + " in {origin} with request: \"{topic}\"", this, topic);
              if (error.StatusCode == HttpStatusCode.BadGateway)
              {
                if (tries > 1)
                {
                  return await QueryRepositoriesByTopic(topic, amount, cursor, tries - 1);
                }

                throw new BadHttpRequestException(e.Message);
              }
              break;
            default:
              _logger.LogError(e.Message + " in {origin} with request: \"{topic}\"", this, topic);
              break;
          }
          throw;
        }
    }
    
    /// <summary>
    /// QueryRepositoryByName sends a graphql request to the github api and returns a repository on success. Does not handle errors
    /// yet
    /// </summary>
    /// <param name="repositoryName">Name of the repository</param>
    /// <param name="ownerName">Name of the repository owner</param>
    /// <returns>repository in the form of RepositoryWrapper</returns>
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

        var response = await _client.SendQueryAsync<RepositoryWrapper>(repositoriesQuery);
        return response.Data;
    }
    
    /// <summary>
    /// ToQueryString converts ProjectRequestDtos into a format that can be inserted into a graphql search query and
    /// sends the query using QueryRepositoriesByName
    /// </summary>
    /// <param name="repos">A list of repository names and owner names</param>
    /// <returns>list of repositories in the form of SpiderData</returns>
    public async Task<SpiderData> GetByNames(List<ProjectRequestDto> repos)
    {
      Queue<ProjectRequestDto> queue = new Queue<ProjectRequestDto>(repos);
      StringBuilder stringBuilder = new StringBuilder();
      List<SpiderData> data = new List<SpiderData>();
      while (queue.Count > 0)
      {
        for (int i = 0; i < 25; i++)
        {
          if (queue.Count == 0)
          {
            break;
          }
          var repo = queue.Dequeue();
          stringBuilder.Append("repo:");
          stringBuilder.Append(repo.OwnerName);
          stringBuilder.Append('/');
          stringBuilder.Append(repo.RepoName);
          stringBuilder.Append(' ');
        }
        
        string query = stringBuilder.ToString();
        data.Add(await QueryRepositoriesByName(query, 25)); 
      }
      
      return (data);
    }
}