
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.Product.Api;
using SaBooBo.Product.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddServiceDefault();

builder
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseServiceDefault();
// Maps all API endpoints

app.MapGet("", () => "Welcome product service." );

app.MapProductApi();
app.MapCategoryApi();

app.Run();