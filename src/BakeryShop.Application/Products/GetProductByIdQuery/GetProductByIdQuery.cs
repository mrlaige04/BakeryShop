using BakeryShop.Application.Common.Abstractions;

namespace BakeryShop.Application.Products.GetProductByIdQuery;
public record GetProductByIdQuery(Guid Id) : IQuery<ProductDto>;
