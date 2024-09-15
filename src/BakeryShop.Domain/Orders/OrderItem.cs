using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Domain.Orders;

public class OrderItem : Entity
{
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }

    public Order Order { get; set; } = null!;
    public Guid OrderId { get; set; }

    private OrderItem() { }

    private OrderItem(Product product, double quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public static OrderItem Create(Product product, double quantity) => new(product, quantity);
}