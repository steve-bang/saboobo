using Microsoft.AspNetCore.Server.Kestrel.Core;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.NotificationService.Apis;
using SaBooBo.NotificationService.Application.WorkerService;
using SaBooBo.NotificationService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

await builder.AddNotificationServices();

builder.Services.AddServiceDefault();

builder.Services.AddHostedService<ZaloOAuthCallbackWorkerService>();
builder.Services.AddHostedService<OrderChangeStatusWorkerService>();

// Settup the Kestrel server 
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2; // Support HTTP/1 and HTTP/2
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapChannelApi();

app.UseServiceDefault();


app.Run();

