using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using spider.Services;

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
        var service = new GitHubGraphqlService(new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer()));
        
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