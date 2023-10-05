using System.Net;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Options;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Spider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false)
    .AddJsonOptions(options =>  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<EcosystemsContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("DevelopmentDb"))
    );
//TODO: figure out appropriate method for adding these service, scoped vs transient vs singleton;
builder.Services.AddScoped<IEcosystemsService, EcosystemsService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<ISpiderService, SpiderService>();
builder.Services.Configure<ElasticsearchClientOptions>(
    options =>
    {
        options.ConfigureSettings += settings => settings.ThrowExceptions();
        options.ConfigureSettings +=
            settings => settings.Authentication(new BasicAuthentication("elastic", "SECODash"));
        // Disable certificate validation, remove in production!
        options.ConfigureSettings += settings => settings.ServerCertificateValidationCallback((_, _, _, _) => true);
    });
builder.Services.AddScoped<IElasticsearchService, ElasticsearchService>();
builder.Services.AddElasticsearchClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.CreateDbIfNotExists();
app.Run();