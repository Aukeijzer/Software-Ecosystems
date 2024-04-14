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

using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Hangfire;
using Hangfire.AspNetCore;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Logging;
using SECODashBackend.Services.Analysis;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Scheduler;
using SECODashBackend.Services.Spider;
using SECODashBackend.Services.Users;

var builder = WebApplication.CreateBuilder(args);
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
            policy =>
            {
                policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();;
            });
});

//Set connection strings
if (Environment.GetEnvironmentVariable("Docker_Environment") == null)
{
    string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString() + "/secrets/backend-connectionstrings.json";
    builder.Configuration.AddJsonFile(path);
}
else
{
    string? filePath = Environment.GetEnvironmentVariable("backend-secrets");
    var backendsecrets = File.OpenRead(filePath);
    builder.Configuration.AddJsonStream(backendsecrets);
}


// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
    .AddJsonOptions(options =>  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<EcosystemsContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DevelopmentDb")));
builder.Services.AddScoped<IEcosystemsService, EcosystemsService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<UsersService>();

var spiderConnectionString = builder.Configuration.GetConnectionString("Spider");
if (string.IsNullOrEmpty(spiderConnectionString))
{
    throw new InvalidOperationException("Missing configuration for Spider");
}

builder.Services.AddScoped<ISpiderService>(_ => new SpiderService(builder.Configuration.GetConnectionString("Spider")!));

var dataProcessorConnectionString = builder.Configuration.GetConnectionString("DataProcessor");
if (string.IsNullOrEmpty(dataProcessorConnectionString))
{
    throw new InvalidOperationException("Missing configuration for Data Processor");
}

builder.Services.AddScoped<IDataProcessorService>(_ => new DataProcessorService(builder.Configuration.GetConnectionString("DataProcessor")!));

ElasticsearchClientSettings settings;
if (Environment.GetEnvironmentVariable("Docker_Environment") == null)
{
    //If ran locally we try to connect to a remote elasticsearch database
    var apiKey = builder.Configuration.GetSection("Elasticsearch").GetSection("ApiKey").Value;
    var cloudId = builder.Configuration.GetSection("Elasticsearch").GetSection("CloudId").Value;
    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(cloudId));
    {
        throw new InvalidOperationException("Missing configuration for Elasticsearch");
    }
    settings = new ElasticsearchClientSettings(cloudId, new ApiKey(apiKey))
        // set default index for ProjectDtos
        .DefaultMappingFor<ProjectDto>(i => i
            .IndexName("projects-03")
        );
}
else
{
    //If ran in docker we try to connect to the local elasticsearch container
    var nodeStrings = builder.Configuration.GetSection("Elasticsearch").GetSection("Nodes").Get<string[]>();
    var password = builder.Configuration.GetSection("Elasticsearch").GetSection("Password").Value;
    var fingerprint = builder.Configuration.GetSection("Elasticsearch").GetSection("Fingerprint").Value;
    Console.WriteLine(builder.Configuration.GetSection("Elasticsearch").Value);
    if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fingerprint) 
        || nodeStrings == null || nodeStrings.Length == 0)
    {
        throw new InvalidOperationException("Missing configuration for Elasticsearch");
    }
    
    var nodes = new Uri[nodeStrings.Length];
    for(int i = 0; i<nodeStrings.Length; i++) {
        nodes[i] = new Uri(nodeStrings[i]);
    }
    var pool = new StaticNodePool(nodes);
    
    settings = new ElasticsearchClientSettings(pool)
        .CertificateFingerprint(fingerprint)
        .Authentication(new BasicAuthentication("elastic", password))
        // set default index for ProjectDtos
        .DefaultMappingFor<ProjectDto>(i => i
            .IndexName("projects-03")
        );
}
builder.Services.AddSingleton(new ElasticsearchClient(settings));
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();
builder.Services.AddScoped<IAnalysisService, ElasticsearchAnalysisService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddFileLogger(options => { builder.Configuration.GetSection("Logging").GetSection("File")
    .GetSection("Options").Bind(options); });


// Configure the Hangfire scheduler
builder.Services.AddHangfire((provider, config) => config
    .UsePostgreSqlStorage(c => c
        .UseNpgsqlConnection(builder.Configuration.GetConnectionString("Hangfire")))
    .UseActivator(new AspNetCoreJobActivator(provider.GetRequiredService<IServiceScopeFactory>()))
    .UseRecommendedSerializerSettings()
    .UseSimpleAssemblyNameTypeSerializer()
    .UseDashboardMetric(DashboardMetrics.FailedCount)
    .UseDashboardMetrics(DashboardMetrics.RecurringJobCount)
    .UseDashboardMetrics(DashboardMetrics.RetriesCount));

// Configure the Hangfire scheduler to retry failed jobs three times with a delay of 1 hour, 6 hours and 12 hours.
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
{
    Attempts = 3, 
    DelaysInSeconds = [60 * 60 * 1, 60 * 60 * 6, 60 * 60 * 12]
});

// Add the Hangfire server that is responsible for executing the scheduled jobs.
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IScheduler, HangfireScheduler>();

var app = builder.Build();

// Use swagger if not in production
if ( Environment.GetEnvironmentVariable("Docker_Enviroment") != "server")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
bool local = Environment.GetEnvironmentVariable("Docker_Environment") == "local";
if ( app.Environment.IsDevelopment() || local )

app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();

// Add a Hangfire dashboard that allows to view and manage the scheduled jobs.
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new [] { new HangfireAuthorizationFilter() }
});

// Configure the endpoints.
app.MapControllers();
app.MapHangfireDashboard();

app.CreateDbIfNotExists();
app.ScheduleInitialJobs();
app.Run();

// Necessary for integration testing.
// See https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program { }