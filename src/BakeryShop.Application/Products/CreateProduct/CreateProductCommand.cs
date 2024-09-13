using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Products.CreateProduct;
public record CreateProductCommand(
    string Title, 
    decimal Price,
    Guid CurrencyId,
    double Quantity, 
    QuantityType QuantityType,
    string? Description) : ICommand<Guid>;