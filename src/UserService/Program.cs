using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.UserService.Apis;
using SaBooBo.UserService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddServices();

builder.Services.AddGlobalExceptionHandler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

// Register the APIs
app.MapAuthApi();
app.MapUserApi();

app.UseAuthentication();
app.UseAuthorization();

app.Run();


