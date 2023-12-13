using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using spider.Converter;
using spider.Logging;
using spider.Services;
using spider.Wrappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
var token = Environment.GetEnvironmentVariable("API_Token");
_client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
_client.HttpClient.DefaultRequestHeaders.Add("X-Github-Next-Global-ID", "1");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IClientWrapper>(new ClientWrapper(_client));
builder.Services.AddScoped<IGitHubGraphqlService, GitHubGraphqlService>();
builder.Services.AddScoped<IGraphqlDataConverter, GraphqlDataConverter>();
builder.Services.AddScoped<IGitHubRestService, GitHubRestService>();
builder.Services.AddScoped<ISpiderProjectService, SpiderProjectService>();
builder.Logging.AddFileLogger(options => { builder.Configuration.GetSection("Logging").GetSection("File")
    .GetSection("Options").Bind(options); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
