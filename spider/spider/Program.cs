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

using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using RestSharp;
using spider.Converters;
using spider.Logging;
using spider.Services;
using spider.Wrappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _client = new GraphQLHttpClient("https://api.github.com/graphql", new SystemTextJsonSerializer());
string? token;
if (Environment.GetEnvironmentVariable("Docker_Enviroment") == null)
    token = Environment.GetEnvironmentVariable("API_Token");
else
{
    string? tokenPath = Environment.GetEnvironmentVariable("API_Token_File");
    token = File.ReadAllText(tokenPath);
}

_client.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
_client.HttpClient.DefaultRequestHeaders.Add("X-Github-Next-Global-ID", "1");

var options = new RestClientOptions("https://api.github.com");
var _gitHubRestClient = new RestClient(options);
_gitHubRestClient.TryAddWithoutValidation("Authorization", "Bearer " + token);
_gitHubRestClient.AddDefaultHeader("X-Github-Next-Global-ID", "1");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRestClient>(_gitHubRestClient);
builder.Services.AddSingleton<IClientWrapper>(new ClientWrapper(_client));
builder.Services.AddScoped<IGitHubGraphqlService, GitHubGraphqlService>();
builder.Services.AddScoped<IGraphqlDataConverter, GraphqlDataConverter>();
builder.Services.AddScoped<IGitHubRestService, GitHubRestService>();
builder.Services.AddScoped<ISpiderProjectService, SpiderProjectService>();
builder.Logging.AddFileLogger(options => { builder.Configuration.GetSection("Logging").GetSection("File")
    .GetSection("Options").Bind(options); });

var app = builder.Build();

bool local = Environment.GetEnvironmentVariable("Docker_Enviroment") == "local";
if ( app.Environment.IsDevelopment() || local ) {   
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
