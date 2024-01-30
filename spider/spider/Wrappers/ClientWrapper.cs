// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

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
    private Logger<ClientWrapper> _logger;
    public IGraphQLWebSocketClient Client { get; set; }
    
    public ClientWrapper(IGraphQLWebSocketClient client)
    {
        Client = client;
        _logger = new Logger<ClientWrapper>(new LoggerFactory());
    }
    
    /// <summary>
    /// SendQueryAsync sends a query to the GitHub GraphQL API. If the request fails it will check the response headers
    /// for the Retry-After header and wait that amount of seconds before retrying the request.
    /// </summary>
    /// <param name="request">The graphql request</param>
    /// <typeparam name="TResponse">The response type</typeparam>
    /// <returns>The response from the graphQL API in the form of GraphQLResponse&lt;TResponse></returns>
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