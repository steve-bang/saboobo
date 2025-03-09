using System.Text.Json.Serialization;
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