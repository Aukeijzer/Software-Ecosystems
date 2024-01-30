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

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using spider.Services;
using spider.Wrappers;

namespace spiderIntegrationTests;

/// <summary>
/// This class is used to create a WebApplicationFactory for the integration tests.
/// </summary>
/// <typeparam name="TProgram"></typeparam>
public class SpiderWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        
        // This is the new service that will be used instead of the default GitHubGraphqlService.
        var service = new GitHubGraphqlService(new ClientWrapper(new GraphQLHttpClient(
            "https://api.github.com/graphql", new SystemTextJsonSerializer())));
        
        builder.ConfigureTestServices(services =>
        {
            // Remove the default GitHubGraphqlService from the service collection.
            var spiderGithubGraphqlServiceDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(GitHubGraphqlService));
            services.Remove(spiderGithubGraphqlServiceDescriptor);
            
            // Add the new service to the service collection.
            services.AddSingleton(service);
        });
    }
}