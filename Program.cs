using Microsoft.EntityFrameworkCore;
using Device_Management.Models;
using Azure.Messaging.ServiceBus;
using Device_Management.Services;
using System.Configuration;
using Device_Management.Services.Email;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.CodeAnalysis.Emit;

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

var dbConnection = String.Empty;
var serviceBusConnection = String.Empty;
var emailConnection = String.Empty;
//builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
//connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    dbConnection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
    serviceBusConnection = builder.Configuration.GetConnectionString("AZURE_SERVICE_BUS_SAS_CONNECTIONSTR");
    emailConnection = builder.Configuration.GetConnectionString("AZURE_COMMUNICATION_CONNECTIONSTR");
}
else
{
    dbConnection = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_AZURE_SQL_CONNECTIONSTRING");
}

builder.Services.AddDbContext<DeviceManagementDbContext>(options =>
    options.UseSqlServer(dbConnection));


// Register the EmailOptions in the dependency injection container
builder.Services.Configure<EmailOptions>(options =>
{
    options.ConnectionString = emailConnection;
});

builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddSingleton(new ServiceBusClient(serviceBusConnection));
builder.Services.AddHostedService<ServiceBusReceiverHostedService>();

builder.Services.AddScoped<AlertService>();
builder.Services.AddScoped<DeviceService>();

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
