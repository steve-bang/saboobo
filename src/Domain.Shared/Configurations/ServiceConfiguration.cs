namespace SaBooBo.Domain.Shared.Configurations;

public class ServiceConfiguration
{
    public ServiceEndpoints ServiceEndpoints { get; set; } = new();
}

public class ServiceEndpoints
{
    public string UserGrpc { get; set; } = "http://user-api-service:50051";
    public string MerchantGrpc { get; set; } = "http://merchant-api-service:50051";

    public ServiceEndpoints()
    {
    }
} 