using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Items;

public class Product : Entity
{
    public string Title { get; set; } = null!;
    public decimal Price { get; private set; }
    
    public double Quantity { get; set; }
    public QuantityType QuantityType { get; set; }
}