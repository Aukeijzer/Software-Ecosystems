using System;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;

namespace spider
{
internal class Program
{
    private const string Token = "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";
    private static readonly ProductHeaderValue ProductInformation = new ProductHeaderValue("Git_Test_Repo", "1.0");

    public static void Main(string[] args)
    {
        Connection conn = new Connection(ProductInformation, Token);
        RunQuery(conn);
        //RateLimitCheck(conn);
        Console.ReadLine();
    }

    private static async void RunQuery(Connection conn)
    {
        var repositoriesQuery = new Query()
            .Search(query: "Git Test Repo", SearchType.Repository, first: 10)
            .Select(x=> new 
            {
                x.RepositoryCount, nds = x.Nodes.Select(node => new
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

        var result = await conn.Run(repositoriesQuery);
        foreach (var nde in result.nds)
        {
            var res = nde.NodeData;
            var repositoryReadMeQuery = new Query()
                .Repository(res.Name, res.Owner)
                .Object(expression: "HEAD:README.md");
            var readMeResponse = await conn.Run(repositoryReadMeQuery);
            Console.WriteLine(res.ToString());
            Console.WriteLine(readMeResponse.ToString());
        }
    }

    private static async void RateLimitCheck(Connection conn)
    {
        var rateLimitCheck = new Query().RateLimit();
        var rateLimit = await conn.Run(rateLimitCheck);
        Console.WriteLine(rateLimit.ToString());
    }
}
}


class RepositoryData
{
public readonly string Name;
public readonly string Owner;
//private readonly ID _id;
public RepositoryData(string name, string owner /*, ID id*/)
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