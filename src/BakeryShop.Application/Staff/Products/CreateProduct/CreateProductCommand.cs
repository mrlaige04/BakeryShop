using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.CreateProduct;
public record CreateProductCommand(
    string Title,
    decimal Price,
    double Quantity,
    QuantityType QuantityType,
    ICollection<InformationDto>? Information,
    string? Description) : ICommand<Guid>;