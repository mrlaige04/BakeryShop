using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Staff.Products.DeleteProduct;
public record DeleteProductCommand(Guid Id) : ICommand;
