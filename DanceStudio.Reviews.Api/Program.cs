using DanceStudio.Reviews.Api.Middleware;
using DanceStudio.Reviews.Application;
using DanceStudio.Reviews.Infrastructure;
using Microsoft.Extensions.DependencyInjection; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["MongoDbSettings:ConnectionString"];

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("MongoDbSettings:ConnectionString is missing in appsettings.json");
}

builder.Services.AddHealthChecks()
    .AddMongoDb(
        sp => new MongoDB.Driver.MongoClient(connectionString),
        name: "mongo",
        timeout: TimeSpan.FromSeconds(3)
    );


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();