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
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;

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

//TODO: configure authentication below to only accept certain urls/certs.
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate(options =>
    {
        options.Events = new CertificateAuthenticationEvents
        {
            OnCertificateValidated = context =>
            {
                var claims = new[]
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        context.ClientCertificate.Subject,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer),
                    new Claim(
                        ClaimTypes.Name,
                        context.ClientCertificate.Subject,
                        ClaimValueTypes.String, context.Options.ClaimsIssuer)
                };
                context.Principal = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, context.Scheme.Name));
                context.Success();
                return Task.CompletedTask;
            }
        };
    });

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

// TODO: WARNING move elasticsearch authentication secrets out of appsettings.json
var apiKey = builder.Configuration.GetSection("Elasticsearch").GetSection("ApiKey").Value;
var cloudId = builder.Configuration.GetSection("Elasticsearch").GetSection("CloudId").Value;
if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(cloudId))
{
    throw new InvalidOperationException("Missing configuration for Elasticsearch");
}
var settings = new ElasticsearchClientSettings(cloudId, new ApiKey(apiKey))
    // set default index for ProjectDtos
    .DefaultMappingFor<ProjectDto>(i => i
        .IndexName("projects-03")
    );

builder.Services.AddSingleton(
    new ElasticsearchClient(settings));
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

// Configure the Hangfire scheduler to retry failed jobs three times with a delay of 2 minutes, 1 hour, and 12 hours.
GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute
{
    Attempts = 3, 
    DelaysInSeconds = [120, 60 * 60 * 1, 60 * 60 * 12]
});

// Add the Hangfire server that is responsible for executing the scheduled jobs.
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IScheduler, HangfireScheduler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
bool local = Environment.GetEnvironmentVariable("Docker_Enviroment") == "local";
if ( app.Environment.IsDevelopment() || local )

// TODO: turn on HttpsRedirection when https is fixed
//app.UseHttpsRedirection();

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