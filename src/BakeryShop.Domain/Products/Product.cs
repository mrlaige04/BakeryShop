using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Products;

public class Product : Entity
{
    public string Title { get; private set; } = null!;
    public decimal Price { get; private set; }
    
    public string? Description { get; private set; }

    public double Quantity { get; private set; }
    public QuantityType QuantityType { get; private set; }

    public ICollection<Information> Information { get; private set; } = [];

    private Product(){ }
    private Product(
        string title, 
        decimal price, 
        string? description,
        double quantity, 
        QuantityType quantityType)
    {
        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
        QuantityType = quantityType;
    }

    public void Update(
        string title, 
        decimal price, 
        string? description,
        double quantity,
        QuantityType quantityType,
        ICollection<Information> information)
    {
        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
        QuantityType = quantityType;

        ClearInformation();
        AddInformationRange(information);
    }

    public void AddInformation(Information information) => Information.Add(information);
    public void RemoveInformation(Information information) => Information.Remove(information);
    public void ClearInformation() => Information.Clear();
    private void AddInformationRange(ICollection<Information> information)
    {
        foreach (var info in information)
        {
            AddInformation(info);
        }
    }

    public static Product Create(
        string title, 
        decimal price, 
        string? description, 
        double quantity,
        QuantityType quantityType)
        => new(title, price, description, quantity, quantityType);
}