using Microsoft.EntityFrameworkCore;
using SaBooBo.Domain.Shared.Extentions;
using SaBooBo.MediaService;
using SaBooBo.MediaService.Repositories;
using SaBooBo.MediaService.Services;
using Microsoft.Extensions.Hosting;
using SaBooBo.MigrationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<IMediaService, SaBooBo.MediaService.Services.MediaService>();

builder.Services.AddScoped<IMediaRepository, MediaRepository>();

builder.Services.AddDbContext<MediaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//builder.EnrichNpgsqlDbContext<MediaDbContext>();

// Add the migration service. When the application starts, it will check if the database is up to date. 
// If not, it will run the migration to update the database.
builder.Services.AddMigration<MediaDbContext>();

builder.Services.AddServiceDefault();

// Config cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseServiceDefault();

// Use cors
app.UseCors("AllowAll");

app.Run();

