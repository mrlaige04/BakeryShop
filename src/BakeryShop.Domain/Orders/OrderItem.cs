using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Items;

namespace BakeryShop.Domain.Orders;

public class OrderItem : Entity
{
    public Product Product { get; set; } = null!;
    public double Quantity { get; set; }

    private OrderItem() { }

    private OrderItem(Product product, double quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public static OrderItem Create(Product product, double quantity) => new(product, quantity);
}