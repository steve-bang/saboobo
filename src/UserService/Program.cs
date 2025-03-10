using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SaBooBo.Domain.Shared.Clients;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.UserService.Apis;
using SaBooBo.UserService.Extensions;
using SaBooBo.UserService.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddGrpc();

// Register the clients
builder.Services.AddHttpClient(ClientNames.Saboobo, (serviceProvider, client) =>
{

    var settings = serviceProvider.GetRequiredService<IConfiguration>();

    //client.DefaultRequestHeaders.Add("Authorization", settings["Clients:"]);

    string? baseUrl = settings["Clients:BaseUrl"];

    if (baseUrl is null)
    {
        throw new ArgumentNullException("Please provide the base url for the client in the appsettings.json file at Clients:BaseUrl");
    }

    client.BaseAddress = new Uri(baseUrl);
});

builder.AddServices();

builder.Services.AddServiceDefault();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Settup the Kestrel server 
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

app.UseServiceDefault();

// Register the APIs
app.MapAuthApi();
app.MapUserApi();

app.UseAuthentication();
app.UseAuthorization();

// Map gRPC services
app.MapGrpcService<UserGrpcService>();

app.Run();


