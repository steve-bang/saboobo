using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.MerchantService.Apis;
using SaBooBo.MerchantService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddMerchantService();

builder.Services.AddServiceDefault();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapMerchantApi();

app.UseServiceDefault();

app.Run();