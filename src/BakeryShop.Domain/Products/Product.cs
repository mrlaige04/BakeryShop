using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Products;

public class Product : Entity
{
    public string Title { get; private set; } = null!;
    public decimal Price { get; private set; }
    
    public string? Description { get; private set; }

    public Currency Currency { get; private set; } = null!;
    public Guid CurrencyId { get; private set; }

    public double Quantity { get; private set; }
    public QuantityType QuantityType { get; private set; }

    private Product(){ }
    private Product(string title, decimal price, string? description, Currency currency, double quantity, QuantityType quantityType)
    {
        Title = title;
        Description = description;
        Price = price;
        Currency = currency;
        Quantity = quantity;
        QuantityType = quantityType;
    }

    public static Product Create(
        string title, 
        decimal price, 
        string? description, 
        Currency currency, 
        double quantity,
        QuantityType quantityType)
        => new(title, price, description, currency, quantity, quantityType);
}