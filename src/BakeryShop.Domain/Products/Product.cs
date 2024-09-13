using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Products;

public class Product : Entity
{
    public string Title { get; set; } = null!;
    public decimal Price { get; private set; }
    
    public string? Description { get; set; }

    public double Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
}