using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.UpdateProduct;
public record UpdateProductCommand(
    string Title,
    decimal Price,
    double Quantity,
    QuantityType QuantityType,
    ICollection<Information> Information,
    string Description) : ICommand<ProductDto>
{
    public Guid Id { get; set; }
}
