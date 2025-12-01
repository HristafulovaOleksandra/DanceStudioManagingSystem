using AutoMapper;
using DanceStudio.Booking.Bll.Profiles;
using DanceStudio.Booking.Bll.Services;
using DanceStudio.Booking.DAL;
using DanceStudio.Booking.DAL.Repositories;
using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Catalog.DAL.Repositories;
using DanceStudio.Catalog.Domain.Interfaces;
using DanceStudioManagingSystem.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<
    DanceStudio.Booking.DAL.Repositories.Interfaces.IUnitOfWork,
    DanceStudio.Booking.DAL.Repositories.UnitOfWork
>();


var connectionString = builder.Configuration.GetConnectionString("BookingDb");


builder.Services.AddScoped<NpgsqlConnection>(sp =>
{
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    return connection;
});

//Catalog Service's Unit of Work
builder.Services.AddScoped<
    DanceStudio.Catalog.Domain.Interfaces.IUnitOfWork,
    DanceStudio.Catalog.DAL.Repositories.UnitOfWork
>();


builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IBookingItemService, BookingItemService>();
builder.Services.AddScoped<IBookingPaymentService, BookingPaymentService>();

builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<IDanceClassRepository, DanceClassRepository>();
builder.Services.AddScoped<IDanceClassDetailRepository, DanceClassDetailRepository>();

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();