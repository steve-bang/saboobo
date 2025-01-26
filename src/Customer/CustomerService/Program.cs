using System.Text.Json.Serialization;
using CustomerService.Extensions;
using Microsoft.AspNetCore.Http.Json;
using SaBooBo.CustomerService.WebApi;
using SaBooBo.Domain.Shared.Extentions;

using MvcJsonOptions = Microsoft.AspNetCore.Mvc.JsonOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddServiceDefault();

builder.AddCustomerService();

builder.Services.Configure<JsonOptions>(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.Configure<MvcJsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseServiceDefault();

app.MapCustomerApi();
app.MapMerchantApi();

app.Run();

