using spider.Converter;
using spider.Logging;
using spider.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGitHubGraphqlService, GitHubGraphqlService>();
builder.Services.AddScoped<IGraphqlDataConverter, GraphqlDataConverter>();
builder.Services.AddScoped<IGitHubRestService, GitHubRestService>();
builder.Logging.AddFileLogger(options => { builder.Configuration.GetSection("Logging").GetSection("File")
    .GetSection("Options").Bind(options); });

var app = builder.Build();

// Configure the HTTP request pipeline.
// Note: For Docker development swagger is always used. Upon code delivery this may need to be changed.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
