using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Domain.Users;
public class CartItem : Entity
{
    public Product Product { get; set; } = null!;
    public Guid ProductId { get; set; }
    public double Quantity { get; set; }

    public Cart Cart { get; set; } = null!;
    public Guid CartId { get; set; }

    private CartItem() { }

    private CartItem(Product product, double quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public static CartItem Create(Product product, double quantity) => new(product, quantity);
}
