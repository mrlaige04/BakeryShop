using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Users;

namespace BakeryShop.Domain.Orders;

public class Order : Entity
{
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    
    private readonly IList<OrderItem> _items = [];
    
    public OrderStatus Status { get; set; }

    [NotMapped] public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public void AddItem(OrderItem item)
    {
        var orderItem = _items
            .FirstOrDefault(it => it.Product.Id == item.Product.Id);

        if (orderItem is not null)
        {
            orderItem.Quantity += item.Quantity;
            return;
        }
        
        _items.Add(item);
    }
}