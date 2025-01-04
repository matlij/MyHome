using Microsoft.AspNetCore.Mvc;
using MyHome.ApiService.Constants;
using MyHome.ApiService.Extensions;
using MyHome.ApiService.HostedServices;
using MyHome.Core.Models.EnergySupplier;
using MyHome.Core.Repositories;
using MyHome.Core.Repositories.WifiSocket;
using MyHome.Core.Services;
using MyHome.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMemoryCache();

builder.Services.RegisterLocalDependencies(builder.Configuration);

builder.Services.AddHostedService<HeatRegulatorHost>();
//builder.Services.AddHostedService<EnergyConsumptionHost>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("energysupplier/energyprice", async ([FromServices] EnergyPriceService energyPriceService) =>
{
    return await energyPriceService.GetEnergyPriceAsync();
})
.WithName("GetEnergyPrices");

app.MapGet("energysupplier/energymeasurement", async ([FromServices] IRepository<EnergyMeasurement> repository) =>
{
    return await repository.GetByIdAsync(MyHomeConstants.MyTibberHomeId);
})
.WithName("GetLastEnergyMeasurement");

app.MapGet("/wifisocket/{name}/status", async ([FromServices] WifiSocketsService service, string name) =>
{
    return await service.GetStatus(name);
})
.WithName("GetWifiSocketStatus");

app.MapGet("/auditevents", async ([FromServices] IRepository<AuditEvent> repository, [FromQuery]int count) =>
{
    var result = await repository.GetAllAsync();
    return result.Take(count);
})
.WithName("GetAuditEvents");

app.MapDefaultEndpoints();

app.Run();