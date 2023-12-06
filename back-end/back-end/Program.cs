using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Logging;
using SECODashBackend.Models;
using SECODashBackend.Services.Analysis;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Spider;

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

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
    .AddJsonOptions(options =>  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<EcosystemsContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DevelopmentDb"))
    );
builder.Services.AddScoped<IEcosystemsService, EcosystemsService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ISpiderService>(_ => new SpiderService(builder.Configuration.GetConnectionString("Spider")));
builder.Services.AddScoped<IDataProcessorService, DataProcessorService>();

// TODO: WARNING move elasticsearch authentication secrets out of appsettings.json
var settings = new ElasticsearchClientSettings(
        builder.Configuration.GetSection("Elasticsearch").GetSection("CloudId").Value!,
        new ApiKey(builder.Configuration.GetSection("Elasticsearch").GetSection("ApiKey").Value!))
    // set default index for Projects
    .DefaultMappingFor<Project>(i => i
        .IndexName("projects-01")
    );

builder.Services.AddSingleton(
    new ElasticsearchClient(settings));
builder.Services.AddElasticsearchClient();
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();
builder.Services.AddScoped<IAnalysisService, ElasticsearchAnalysisService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddFileLogger(options => { builder.Configuration.GetSection("Logging").GetSection("File")
    .GetSection("Options").Bind(options); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: turn on HttpsRedirection when https is fixed
//app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();
app.CreateDbIfNotExists();
app.Run();

// Necessary for integration testing.
// See https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program { }