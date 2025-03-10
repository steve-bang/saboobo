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
builder.Services.AddHostedService<OrderCompletedWorkerService>();

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

