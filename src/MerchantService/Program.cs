using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.MerchantService.Apis;
using SaBooBo.MerchantService.Extensions;
using SaBooBo.MerchantService.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddMerchantService();

builder.Services.AddServiceDefault();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1; // Support HTTP/1 only
    });

    options.ListenAnyIP(50051, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2; // Support HTTP/2 only
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();



app.MapMerchantApi();
app.MapBannerApi();
app.MapMerchantProviderSettingApi();

app.UseServiceDefault();

// Map gRPC service
app.MapGrpcService<MerchantGrpcService>();

app.Run();