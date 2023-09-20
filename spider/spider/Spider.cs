using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client;
using System.Net;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using GraphQL.Client.Serializer.SystemTextJson;

namespace spider
{
    internal class Spider
    {
        private static bool _queryFinished;
        private const string Token = "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";
        private const string UserLogin = "Secodash";

        public static SpiderData Main()
        {
            var graphQLClient = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            _queryFinished = false;
            SpiderData result =  RunQuery(graphQLClient).Result;
            //RateLimitCheck(conn);
            while (!_queryFinished)
            {
                Console.ReadLine();
            }

            return result;

        }

        private static async Task<SpiderData> RunQuery(GraphQLHttpClient client)
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
                Variables = new{name="API_Test_Repo", fileName = "main:README.md"}
            };
            var resultString = await client.SendQueryAsync<Object>(repositoriesQuery);
            var result = await client.SendQueryAsync(repositoriesQuery, () => new SpiderData());
            Console.WriteLine(result.Data);
            _queryFinished = true;
            return result.Data;
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
    }

        public class SpiderData
        {
            public SearchResult search { get; set; }
        }
        public class SearchResult
        {
            public int repositoryCount { get; set; }
            public Repository[] nodes { get; set; }
        }

        public class RepositoryWrapper
        {
            public Repository repository { get; set; }
        }
        
        public class Repository
        {
            public string name { get; set; }
            public Owner owner { get; set; }
            public TopicsWrapper repositoryTopics { get; set; }
            public string readmeText { get; set; }
        }

        public class Owner
        {
            public string login { get; set; }
        }

        public class TopicsWrapper
        {
            public Topic[] topics { get; set; }
        }

        public class Topic
        {
            public string name { get; set; }
        }
}