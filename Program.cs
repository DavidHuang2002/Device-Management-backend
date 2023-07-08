using Microsoft.EntityFrameworkCore;
using Device_Management.Models;

var builder = WebApplication.CreateBuilder(args);


// config for using local SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DeviceManagementDB");
//builder.Services.AddDbContextPool<DeviceManagementDbContext>(option =>
//    option.UseSqlServer(connectionString)
//);

// config for using inMemory DB for testing
builder.Services.AddDbContext<DeviceManagementDbContext>(opt =>
    opt.UseInMemoryDatabase("TestDb"));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
