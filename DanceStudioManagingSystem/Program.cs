using DanceStudioManagingSystem.Middleware; 
using DanceStudio.Booking.Bll.Profiles;
using DanceStudio.Booking.Bll.Services;

using DanceStudio.Booking.DAL;
using Npgsql;
using DanceStudio.Booking.DAL.Repositories.Interfaces;
using DanceStudio.Booking.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("BookingDb");


builder.Services.AddScoped<NpgsqlConnection>(sp =>
{
    var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    return connection;
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);



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