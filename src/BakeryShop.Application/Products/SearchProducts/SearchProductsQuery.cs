using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Models;

namespace BakeryShop.Application.Products.SearchProducts;
public record SearchProductsQuery(
    string? Query,
    decimal? PriceFrom,
    decimal? PriceTo,
    int? PageNumber,
    int? PageSize) : IQuery<PaginatedList<ProductDto>>;
