using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Users;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BakeryShop.Domain.Orders;

public class Order : Entity
{
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    
    private readonly IList<OrderItem> _items = [];
    
    public OrderStatus Status { get; set; }

    public string? AdditionalInfo { get; set; }

    [NotMapped] public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public DeliveryInfo DeliveryInfo { get; set; } = null!;

    public static Order Create(IList<OrderItem> items, DeliveryInfo deliveryInfo)
    {
        var order = new Order
        {
            DeliveryInfo = deliveryInfo
        };

        foreach (var item in items)
        {
            order.AddItem(item);
        }

        order.Status = OrderStatus.Created;

        return order;
    }

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