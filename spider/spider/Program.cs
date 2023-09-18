using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client;
using System.Net;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using GraphQL.Client.Serializer.SystemTextJson;
using Newtonsoft.Json;

namespace spider
{
    internal class Program
    {
        private const string Token = "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";
        private const string UserLogin = "Secodash";
        //private static readonly ProductHeaderValue ProductInformation = new ProductHeaderValue("Git_Test_Repo", "1.0");

        public static void Main(string[] args)
        {
            var graphQLClient = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            //Connection conn = new Connection(ProductInformation, Token);
            RunQuery(graphQLClient);
            //RateLimitCheck(conn);
            Console.ReadLine();
        }

        private static async void RunQuery(GraphQLHttpClient client)
        {
            /*
            var repositoriesQuery = new Query()
                .Search(query: "API_Test_Repo", SearchType.Repository, first: 1)
                .Select(x=> new
                {
                    x.RepositoryCount,
                    nds = x.Nodes.Select(node => new
                    {
                        NodeData = node.Switch<RepositoryData>(
                            when => when.Repository(
                            repo => new RepositoryData(
                                repo.Name,
                                repo.Owner.Login
                                //repo.Id
                                )))
                    }).ToList()

                });
            */
            
            var repositoriesQuery = new GraphQLHttpRequest()
            {
                Query = @"query repositoriesQueryRequest($name: String!, $fileName : String!){
                            search(query: $name, type: REPOSITORY, first: 10) {
                            repositoryCount
                             nodes {
                                __typename
                                ... on Repository {
                                    name
                                    owner{
                                        login
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

            var result = await client.SendQueryAsync<Object>(repositoriesQuery);
            Console.WriteLine(result.Data);
            var nds = "";
            foreach (var nde in nds)
            {
            /*
            var res = nde.NodeData;
            //Console.WriteLine(res.ToString());
            var repositoryReadMeQuery = new Query()
                .Repository( res.Name, res.Owner )
                .Object(expression: "main:README.md")
                .Select(x => x);
            */

            var repositoryReadMeQuery = new GraphQLHttpRequest()
            {
                Query = @"query RepositoryReadMeQueryRequest($name : String!, $owner : String!, $fileName : String!) {
                                repository(name: $name, owner: $owner){
                                    name
                                    owner{
                                        login
                                    }
                                    object(expression: $fileName){
                                        ... on Blob{
                                            text
                                            }
                                        }
                                    }
                                }",
                OperationName = "RepositoryReadMeQueryRequest",
                Variables = new { name = "API_Test_Repo", owner = "Secodash", fileName = "main:README.md" }
            };

            //var repositoryReadMeQueryResponse = await client.SendQueryAsync(repositoryReadMeQuery, () => new {Data = new RepoReadMeData() });
            var repositoryReadMeQueryResponse = await client.SendQueryAsync<Object>(repositoryReadMeQuery);
            Console.WriteLine(repositoryReadMeQueryResponse.Data.ToString()); 
            }

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
        /*
        class RepositoryData
        {
            public string Name { get; set; }

            public string Owner { get; set; }

            //private readonly ID _id;
            public RepositoryData(string name, string owner , ID id)
            {
                this.Name = name;
                this.Owner = owner;
                //this._id = id;
            }

            public override string ToString()
            {
                return "Name: " + Name + " || " + "Owner: " + Owner; // + " || ID: " + _id;
            }
        }
        */
        class RepoReadMeDataWrapper
        {
            public RepoReadMeData Data { get; set; }
        }
        
        }
        class RepoReadMeData
        {
            public string Name { get; set; }
            public string Owner{ get; set; }

            public string ReadMeText{ get; set; }
            
            /*
            public RepoReadMeData(string name, string owner, string text)
            {
                this.Name = name;
                this.Owner = owner;
                this.ReadMeText = text;
            }
            */
            
            public override string ToString()
            {
                return "Name: " + Name + " || " + "Owner: " + Owner + " || Text: " + ReadMeText;
            }
        }
    }