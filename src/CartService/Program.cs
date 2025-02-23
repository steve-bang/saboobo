using SaBooBo.CartService.Apis;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.MerchantService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddServiceDefault();
await builder.AddCartService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapCartApi();

app.UseServiceDefault();

app.Run();
