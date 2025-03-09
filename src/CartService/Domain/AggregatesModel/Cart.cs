
namespace SaBooBo.CartService.Domain.AggregatesModel
{
    public class Cart : AggregateRoot
    {
        private List<CartItem> _items = new();
        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public Cart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            _items = new List<CartItem>();
            TotalPrice = 0;
            CreatedAt = DateTime.UtcNow.ToUniversalTime();
            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }

        public void AddItem(Guid productId, string productName, string productImage, decimal price, int quantity, string? notes)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.AddQuantity(quantity);
            }
            else
            {
                _items.Add(new CartItem(productId, productName, productImage, quantity, price, notes));
            }

            TotalPrice += price * quantity;
            UpdatedAt = DateTime.UtcNow.ToUniversalTime();
        }

        public void RemoveItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == itemId);
            if (item != null)
            {
                TotalPrice -= item.UnitPrice * item.Quantity;
                _items.Remove(item);
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void UpdateItem(Guid productId, int quantity, string? notes)
        {
            var item = Items.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
            {
                item.UpdateNotes(notes);

                TotalPrice -= item.UnitPrice * item.Quantity;
                item.UpdateQuantity(quantity);
                TotalPrice += item.UnitPrice * item.Quantity;
                UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}