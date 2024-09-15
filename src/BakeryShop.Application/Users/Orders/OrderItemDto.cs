using BakeryShop.Application.Products;

namespace BakeryShop.Application.Users.Orders;
public class OrderItemDto
{
    public ProductDto Product { get; set; } = null!;
    public double Quantity { get; set; }
}
