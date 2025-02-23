using SaBooBo.Domain.Shared.Clients;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.UserService.Apis;
using SaBooBo.UserService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

app.Run();


