using BakeryShop.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BakeryShop.Domain.Users;

public class Cart : Entity
{
    private readonly IList<CartItem> _items = [];

    public Guid OwnerId { get; set; }

    [NotMapped] public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public void AddItem(CartItem item)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existing is not null)
        {
            existing.Quantity += item.Quantity;
            return;
        }

        _items.Add(item);
    }

    public void RemoveItem(CartItem item)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == item.ProductId);
        if (existing is not null)
        {
            existing.Quantity -= item.Quantity;
            if (existing.Quantity <= 0)
                _items.Remove(existing);
        }
    }

    public void ClearCart() => _items.Clear();
}