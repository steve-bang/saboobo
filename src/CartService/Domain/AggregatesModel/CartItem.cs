

namespace SaBooBo.CartService.Domain.AggregatesModel
{
    public class CartItem : AggregateRoot
    {
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice  => UnitPrice * Quantity;
        public string? Notes { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow.ToUniversalTime();


        public CartItem(Guid productId, string productName, decimal unitPrice, int quantity, string? notes)
        {
            Id = Guid.NewGuid();

            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            Notes = notes;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void UpdateNotes(string? notes)
        {
            Notes = notes;
        }

        public void Update(string productName, decimal price, int quantity, string notes)
        {
            ProductName = productName;
            UnitPrice = price;
            Quantity = quantity;
            Notes = notes;
        }
    }
}