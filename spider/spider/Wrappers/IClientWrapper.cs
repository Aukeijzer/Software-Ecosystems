using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;

namespace spider.Wrappers;

/// <summary>
/// Wrapper for GraphQL client
/// </summary>
public interface IClientWrapper
{
    public IGraphQLWebSocketClient Client { get; set; }

    public Task<GraphQLResponse<TResponse>> SendQueryAsync<TResponse>(GraphQLHttpRequest? request);
}