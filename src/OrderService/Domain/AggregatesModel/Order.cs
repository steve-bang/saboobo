/**
* Author: Steve Bang
* Date: 2025-03-08
* Description: This is the Order aggregate root class.
* The work flow of the status: Pending > Confirm > Shipping > Completed
* 
* 
* 
* 
* 
* 
* 
**/

using SaBooBo.Domain.Shared;
using SaBooBo.OrderService.Domain.Events;

namespace SaBooBo.OrderService.Domain.AggregatesModel;

public class Order : AggregateRoot
{
    private ShippingAddress _shippingAddress = null!;
    private List<OrderItem> _items = new();

    public Guid MerchantId { get; private set; }

    public Guid CustomerId { get; private set; }

    public string ZaloOrderId { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public OrderStatus Status { get; private set; }

    /// <summary>
    /// This is the total of the order before tax and shipping costs are added.
    /// Tổng giá trị của đơn hàng trước khi áp dụng thuế và phí vận chuyển.
    /// </summary>
    public decimal Subtotal { get; private set; }

    /// <summary>
    /// This is the tax total of the order.
    /// Tổng tiền thuế của đơn hàng.
    /// </summary>
    public decimal TaxTotal { get; private set; }

    /// <summary>
    /// This is the shipping total of the order.
    /// Tổng chi phí vận chuyển của đơn hàng.
    /// </summary>
    public decimal ShippingTotal { get; private set; }

    /// <summary>
    /// This is the total cost of the order. The cost total includes the subtotal, tax total, and shipping total.
    /// Tổng chi phí của đơn hàng.
    /// </summary>
    public decimal CostTotal { get; private set; }

    /// <summary>
    /// This is the IP address of the user who placed the order.
    /// </summary>
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


    public Order(Guid merchantId, Guid customerId, string code, string paymentMethod, string ipAddress, string? notes, DateTime estimatedTimeDeliveryFrom, DateTime estimatedTimeDeliveryTo)
    {
        Id = Guid.NewGuid();
        MerchantId = merchantId;
        CustomerId = customerId;
        Code = code;
        PaymentMethod = paymentMethod;
        IpAddress = ipAddress;
        Notes = notes;
        EstimatedTimeDeliveryFrom = estimatedTimeDeliveryFrom.ToUniversalTime();
        EstimatedTimeDeliveryTo = estimatedTimeDeliveryTo.ToUniversalTime();
    }


    public static Order Create(Guid merchantId, Guid customerId, string code, string paymentMethod, string ipAddress, string? notes, DateTime estimatedTimeDeliveryFrom, DateTime estimatedTimeDeliveryTo)
    {
        var order = new Order(merchantId, customerId, code, paymentMethod, ipAddress, notes, estimatedTimeDeliveryFrom, estimatedTimeDeliveryTo);

        order.UpdateStatus(OrderStatus.Pending);

        return order;
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

    public void AddShippingAddress(string name, string phoneNumber, string addressDetail, string? email = null)
    {
        _shippingAddress = ShippingAddress.Create(
            name: name,
            phoneNumber: phoneNumber,
            email: email,
            addressDetail: addressDetail
        );
    }

    /// <summary>
    /// Update the status of the order.
    /// </summary>
    /// <param name="status">The status to update the order to.</param>
    /// <exception cref="InvalidOperationException">Thrown when the status is invalid.</exception>
    private void UpdateStatus(OrderStatus status)
    {
        Status = status;
        OrderStatusLastChangedOn = DateTime.UtcNow;

        switch (status)
        {
            case OrderStatus.Pending:
                AddEvent(OrderCreatedEvent.Create(this)); // TODO: Add event to the event store
                return;
            case OrderStatus.Confirmed:
                AddEvent(OrderConfirmEvent.Create(this)); // TODO: Add event to the event store
                return;
            case OrderStatus.Shipping:
                AddEvent(OrderShippingEvent.Create(this)); // TODO: Add event to the event store
                return;
            case OrderStatus.Completed:
                AddEvent(OrderCompleteEvent.Create(this));
                return;
            default:
                throw new InvalidOperationException($"Invalid order status: {status}");
        }
    }

    private void CalculateTotals()
    {
        Subtotal = _items.Sum(x => x.TotalPrice);
        //TaxTotal = (int)(Subtotal * 0.1m);
        CostTotal = Subtotal + TaxTotal + ShippingTotal;
    }

    private void UpdateShippingTotal(decimal shippingTotal, bool isFreeShipping)
    {
        if (isFreeShipping)
        {
            ShippingTotal = 0;
        }
        else
        {
            ShippingTotal = shippingTotal;
        }
        CalculateTotals();
    }

    public void UpdateOrderConfirmed(
        decimal shippingTotal,
        bool isFreeShipping
    )
    {
        UpdateShippingTotal(shippingTotal, isFreeShipping);
        UpdateStatus(OrderStatus.Confirmed);

        CalculateTotals();
    }

    public void UpdateOrderDelivered()
    {
        UpdateStatus(OrderStatus.Delivered);
    }

    public void UpdateOrderShipping()
    {
        // Checks if the order is already completed
        if (Status != OrderStatus.Pending)
        {
            UpdateStatus(OrderStatus.Shipping);
        }
        else
        {
            throw new InvalidOperationException("Order must be in pending status to be updated to shipping status");
        }
    }

    public void UpdateOrderCompleted()
    {
        // Checks if the order is already completed
        if (Status == OrderStatus.Completed)
        {
            throw new InvalidOperationException("Order is already completed");
        }

        if (Status != OrderStatus.Shipping)
        {
            throw new InvalidOperationException("Order must be in shipping status to be updated to completed status");
        }

        UpdateStatus(OrderStatus.Completed);
    }

    public void UpdateOrderCancelled()
    {
        // Checks if the order is already completed
        if (Status == OrderStatus.Completed)
        {
            throw new InvalidOperationException("Order is already completed");
        }


        UpdateStatus(OrderStatus.Cancelled);
    }
}