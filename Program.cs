using Microsoft.EntityFrameworkCore;
using Device_Management.Models;

var builder = WebApplication.CreateBuilder(args);


// config for using local SQL Server
//var connectionString = builder.Configuration.GetConnectionString("DeviceManagementDB");
//builder.Services.AddDbContextPool<DeviceManagementDbContext>(option =>
//    option.UseSqlServer(connectionString)
//);

// config for using inMemory DB for testing
//builder.Services.AddDbContext<DeviceManagementDbContext>(opt =>
//    opt.UseInMemoryDatabase("TestDb"));

// Add services to the container.

builder.Services.AddControllers();

// Add CORS services.
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
//builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
//connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<DeviceManagementDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy before UseRouting and other middleware
app.UseCors(builder =>
    builder
        .WithOrigins("http://127.0.0.1:5173") // replace with the origin you want to allow
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
