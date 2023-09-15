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
        Console.ReadLine();
    }

    private static async void RunQuery(Connection conn)
    {
        var query = new Query()
            .Search(query: "Git Test Repo", SearchType.Repository, first: 10)
            .Select(x=> new 
            {
                x.RepositoryCount, nds = x.Nodes.Select(node => new
                {
                    NodeData = node.Switch<RepositoryData>(
                        when => when.Repository(
                        repo => new RepositoryData(
                            repo.Name,
                            repo.Owner.Login,
                            repo.Id)))
                }).ToList()
                
            });

        var result = await conn.Run(query);
        foreach (var res in result.nds)
        {
            Console.WriteLine(res.ToString());
        }
    }
}
}


class RepositoryData
{
private readonly string _name;
private readonly string _owner;
private readonly ID _id;
public RepositoryData(string name, string owner, ID id)
{
    this._name = name;
    this._owner = owner;
    this._id = id;
}

public override string ToString()
{
    return "Name: " + _name + " || " + "Owner: " + _owner + " || ID: " + _id;
}
}