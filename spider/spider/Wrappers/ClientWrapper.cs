using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;

namespace spider.Wrappers;

/// <summary>
/// Wrapper for GraphQL.Client.Abstractions.Websocket.IGraphQLWebSocketClient
/// 
/// This is a wrapper because the IGraphQLWebSocketClient interface is not
/// mockable.
/// </summary>
public class ClientWrapper : IClientWrapper
{
    public ClientWrapper(IGraphQLWebSocketClient client)
    {
        Client = client;
    }

    public IGraphQLWebSocketClient Client { get; set; }
    public async Task<GraphQLResponse<TResponse>> SendQueryAsync<TResponse>(GraphQLHttpRequest? request)
    {
        return await Client.SendQueryAsync<TResponse>(request, cancellationToken: default);
    }
}