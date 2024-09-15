using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;

namespace BakeryShop.Application.Staff.Products.SearchProducts;
public record SearchProductQuery(
    string? Query,
    decimal? PriceFrom,
    decimal? PriceTo,
    double? QuantityFrom,
    double? QuantityTo,
    QuantityType? QuantityType,
    int? PageNumber,
    int? PageSize, 
    bool? IncludeInfo = false) : IQuery<PaginatedList<ProductDto>>;
