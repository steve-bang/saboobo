using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.MerchantService.Extensions;
using SaBooBo.OrderService.Apis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

await builder.AddOrderService();

builder.Services.AddServiceDefault();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapOrderApi();

app.UseServiceDefault();

app.Run();

