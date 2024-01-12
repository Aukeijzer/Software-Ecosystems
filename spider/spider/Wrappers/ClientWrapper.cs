using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;

namespace spider.Wrappers;

public class ClientWrapper : IClientWrapper
{
    private Logger<ClientWrapper> _logger;
    public IGraphQLWebSocketClient Client { get; set; }
    
    public ClientWrapper(IGraphQLWebSocketClient client)
    {
        Client = client;
        _logger = new Logger<ClientWrapper>(new LoggerFactory());
    }
    
    public async Task<GraphQLResponse<TResponse>> SendQueryAsync<TResponse>(GraphQLHttpRequest? request)
    {
        try
        {
            return await Client.SendQueryAsync<TResponse>(request, cancellationToken: default);
        }
        catch (Exception e)
        {
            if (e is GraphQLHttpRequestException response)
            {
                var header = response.ResponseHeaders.FirstOrDefault(x => x.Key.ToString() == "X-RateLimit-Remaining");
                
                header = response.ResponseHeaders.FirstOrDefault(x => x.Key.ToString() == "Retry-After");
                if (header.Value != null)
                {
                    int time = int.Parse(header.Value.ElementAt(0));
                    _logger.LogWarning("Secondary rate limit reached. Retrying in {seconds} seconds", time);
                    Thread.Sleep(TimeSpan.FromSeconds(time));
                    return await SendQueryAsync<TResponse>(request);
                }
            }
            
            throw;
        }
    }
}