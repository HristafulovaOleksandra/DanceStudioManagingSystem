using DanceStudio.Booking.Bll.Profiles;
using DanceStudio.Catalog.Api.Middleware;
using DanceStudio.Catalog.Bll.Services;
using DanceStudio.Catalog.Bll.Validators;
using DanceStudio.Catalog.DAL;
using DanceStudio.Catalog.DAL.Repositories;
using DanceStudio.Catalog.Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

var connectionString = builder.Configuration.GetConnectionString("CatalogConnection");
builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IDanceClassRepository, DanceClassRepository>();
builder.Services.AddScoped<IDanceClassDetailRepository, DanceClassDetailRepository>();


builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<IDanceClassService, DanceClassService>();
builder.Services.AddScoped<IDanceClassDetailService, DanceClassDetailService>();


builder.Services.AddAutoMapper(typeof(CatalogMappingProfile).Assembly);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DataSeeder.SeedAsync(services);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while seeding the database.");
    }
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();