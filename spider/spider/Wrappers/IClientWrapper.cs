using System.Linq.Expressions;
using GraphQL;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;

namespace spider.Wrappers;

public interface IClientWrapper
{
    public IGraphQLWebSocketClient Client { get; set; }

    public Task<GraphQLResponse<TResponse>> SendQueryAsync<TResponse>(GraphQLHttpRequest? request);
}