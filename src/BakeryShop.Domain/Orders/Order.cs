using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Orders;

public class Order : Entity
{
    private readonly IList<OrderItem> _orderItems = [];
    
    public OrderStatus Status { get; set; }

    [NotMapped] public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public void AddItem(OrderItem item)
    {
        var orderItem = _orderItems
            .FirstOrDefault(it => it.Product.Id == item.Product.Id);

        if (orderItem is not null)
        {
            orderItem.Quantity++;
            return;
        }
        
        _orderItems.Add(item);
    }
}