using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Logging;
using SECODashBackend.Services.Ecosystems;
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.CreateDbIfNotExists();
app.Run();
