using System.Text.Json.Serialization;

using CLUK.HealthProbes;
using CLUK.Skeleton.API;
using CLUK.Skeleton.API.HealthChecks;
using CLUK.Skeleton.API.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(opts => { opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettings = builder.Configuration
    .GetApplicationSettings();

builder.Services
    .AddApplicationSettings(appSettings)
    .AddCustomHttpLogging();

builder.Services
    .AddDefaultHealthProbes()
    .AddSqlServerHealthCheck(appSettings);

builder.Services
    .RegisterServices();

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseDefaultHealthProbes();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.AddHttpLogging();

app.Run();