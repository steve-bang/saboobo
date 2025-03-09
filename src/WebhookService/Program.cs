using Saboobo.RabbitMqService.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddControllers();

await builder.AddRabbitMQService();

var app = builder.Build();


app.UseHttpsRedirection();

app.MapControllers();

app.Run();
