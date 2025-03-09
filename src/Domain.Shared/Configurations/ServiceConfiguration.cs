namespace SaBooBo.Domain.Shared.Configurations;

public class ServiceConfiguration
{
    public ServiceEndpoints ServiceEndpoints { get; set; } = new();
}

public class ServiceEndpoints
{
    public string NotificationGrpc { get; set; } = "https://localhost:5001";
    public string UserGrpc { get; set; } = "https://user-api-service:8080";
    public string MerchantGrpc { get; set; } = "http://merchant-api-service:8080";
} 