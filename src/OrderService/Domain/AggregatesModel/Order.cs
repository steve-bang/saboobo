
using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Domain.AggregatesModel;

public class Order : AggregateRoot
{
    private ShippingAddress _shippingAddress = null!;
    private List<OrderItem> _items = new();

    public Guid CustomerId { get; private set; }

    public string ZaloOrderId { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public OrderStatus Status { get; private set; }

    public decimal Subtotal { get; private set; }

    public decimal TaxTotal { get; private set; }

    public decimal ShippingTotal { get; private set; }

    public decimal CostTotal { get; private set; }

    public string? IpAddress { get; private set; }

    public string PaymentMethod { get; private set; } = null!;

    public string? Notes { get; private set; }

    public DateTime? OrderStatusLastChangedOn { get; private set; }

    public ShippingAddress ShippingAddress => _shippingAddress;

    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();

    public DateTime EstimatedTimeDeliveryFrom { get; private set; }

    public DateTime EstimatedTimeDeliveryTo { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();

    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();


    public Order(Guid customerId, string code, string paymentMethod, string ipAddress, string? notes, DateTime estimatedTimeDeliveryFrom, DateTime estimatedTimeDeliveryTo)
    {
        Id = Guid.NewGuid();

        CustomerId = customerId;
        Code = code;
        PaymentMethod = paymentMethod;
        IpAddress = ipAddress;
        Notes = notes;
        EstimatedTimeDeliveryFrom = estimatedTimeDeliveryFrom;
        EstimatedTimeDeliveryTo = estimatedTimeDeliveryTo;
        Status = OrderStatus.Pending;

        AddEvent(OrderCreatedEvent.Create(this));
    }


    public static Order Create(Guid customerId, string code, string paymentMethod, string ipAddress, string? notes, DateTime estimatedTimeDeliveryFrom, DateTime estimatedTimeDeliveryTo)
    {
        return new Order(customerId, code, paymentMethod, ipAddress, notes, estimatedTimeDeliveryFrom, estimatedTimeDeliveryTo);
    }
    public void AddItem(Guid productId, string productName, string imageUrl, decimal unitPrice, int quantity, string? notes)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
        }
        else
        {
            _items.Add(new OrderItem(productId, productName, imageUrl, unitPrice, quantity, notes));
        }

        CalculateTotals();
    }

    public void UpdateItem(Guid productId, string productName, decimal unitPrice, int quantity, string notes)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            existingItem.Update(productName, unitPrice, quantity, notes);
        }

        CalculateTotals();
    }

    public void RemoveItem(Guid productId)
    {
        var existingItem = _items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            _items.Remove(existingItem);
        }

        CalculateTotals();
    }

    public void UpdateShippingAddress(string name, string address, string city, string state, string country, string zipCode, string phoneNumber, string email)
    {
        _shippingAddress = new ShippingAddress(name, address, city, state, country, zipCode, phoneNumber, email);
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
        OrderStatusLastChangedOn = DateTime.UtcNow;
    }

    private void CalculateTotals()
    {
        Subtotal = _items.Sum(x => x.TotalPrice);
        TaxTotal = (int)(Subtotal * 0.1m);
        CostTotal = Subtotal + TaxTotal + ShippingTotal;
    }

    public void UpdateShippingTotal(decimal shippingTotal)
    {
        ShippingTotal = shippingTotal;
        CalculateTotals();
    }
}