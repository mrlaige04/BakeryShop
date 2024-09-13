using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Products;
public class ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public double Quantity { get; set; }
    public QuantityType QuantityType { get; set; }

    public static implicit operator ProductDto(Product product) => new()
    {
        Id = product.Id,
        Title = product.Title,
        Description = product.Description,
        Price = product.Price,
        Quantity = product.Quantity,
        QuantityType = product.QuantityType
    };
}
