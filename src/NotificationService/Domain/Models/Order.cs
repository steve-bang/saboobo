using System.Text.Json.Serialization;

namespace SaBooBo.NotificationService.Domain.Models;

public class Order
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("merchantId")]
    public Guid MerchantId { get; set; }

    [JsonPropertyName("customerId")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("zaloOrderId")]
    public string? ZaloOrderId { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("subtotal")]
    public decimal Subtotal { get; set; }

    [JsonPropertyName("taxTotal")]
    public decimal TaxTotal { get; set; }

    [JsonPropertyName("shippingTotal")]
    public decimal ShippingTotal { get; set; }

    [JsonPropertyName("costTotal")]
    public decimal CostTotal { get; set; }

    [JsonPropertyName("ipAddress")]
    public string IpAddress { get; set; } = string.Empty;

    [JsonPropertyName("paymentMethod")]
    public string PaymentMethod { get; set; } = string.Empty;

    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonPropertyName("orderStatusLastChangedOn")]
    public DateTime OrderStatusLastChangedOn { get; set; }

    [JsonPropertyName("shippingAddress")]
    public ShippingAddress ShippingAddress { get; set; } = new();

    [JsonPropertyName("items")]
    public List<OrderItem> Items { get; set; } = new();

    [JsonPropertyName("estimatedTimeDeliveryFrom")]
    public DateTime EstimatedTimeDeliveryFrom { get; set; }

    [JsonPropertyName("estimatedTimeDeliveryTo")]
    public DateTime EstimatedTimeDeliveryTo { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

}

public class ShippingAddress
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("addressDetail")]
    public string AddressDetail { get; set; } = string.Empty;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

}

public class OrderItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("productId")]
    public string ProductId { get; set; } = string.Empty;

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; } = string.Empty;

    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonPropertyName("totalPrice")]
    public decimal TotalPrice { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

} 