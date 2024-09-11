using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Orders;

namespace BakeryShop.Domain.Users;

public class Cart : Entity
{
    private readonly IList<OrderItem> _items = [];

    [NotMapped] public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public void AddItem(OrderItem item)
    {
        
    }

    public void ClearCart() => _items.Clear();
}