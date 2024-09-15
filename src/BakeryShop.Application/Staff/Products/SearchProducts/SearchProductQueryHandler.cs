using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Extensions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Application.Products;
using BakeryShop.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Staff.Products.SearchProducts;
internal sealed class SearchProductQueryHandler(
    IProductRepository productRepository,
    ILogger<SearchProductQueryHandler> logger)
    : IQueryHandler<SearchProductQuery, PaginatedList<ProductDto>>
{
    public async Task<Result<PaginatedList<ProductDto>>> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("SearchProductQuery: Started.");

        var products = productRepository.Source;

        products = ApplyFilters(products, request);

        var pageNumber = ParseIntOrDefault(request.PageNumber?.ToString(), 1);
        var pageSize = ParseIntOrDefault(request.PageSize?.ToString(), 10);

        if (pageNumber < 0)
        {
            logger.LogInformation("SearchProductQuery: Error. Invalid page number.");
            return Result.Error("PageNumber cannot be a negative number");
        }
            
        if (pageSize < 0)
        {
            logger.LogInformation("SearchProductQuery: Error. Invalid page size.");
            return Result.Error("PageSize cannot be a negative number");
        }

        var result = await products
            .Select(product => (ProductDto)product)
            .PaginatedListAsync(pageNumber, pageSize);

        logger.LogInformation("SearchProductQuery: Success.");

        return result;
    }

    private static IQueryable<Product> ApplyFilters(IQueryable<Product> products, SearchProductQuery request)
    {
        if (!string.IsNullOrEmpty(request.Query))
            products = products.Where(p => EF.Functions.Like(p.Title, $"%{request.Query}%"));
        
        if (decimal.TryParse(request.PriceFrom?.ToString(), out decimal priceFrom))
            products = products.Where(p => p.Price >= priceFrom);

        if (decimal.TryParse(request.PriceTo?.ToString(), out decimal priceTo))
            products = products.Where(p => p.Price <= priceTo);

        if (request.QuantityFrom.HasValue)
            products = products.Where(p => p.Quantity >= request.QuantityFrom.Value);

        if (request.QuantityTo.HasValue)
            products = products.Where(p => p.Quantity <= request.QuantityTo.Value);

        if (request.QuantityType.HasValue)
            products = products.Where(p => p.QuantityType == request.QuantityType.Value);

        return products;
    }


    private static int ParseIntOrDefault(string? value, int defaultValue) =>
        int.TryParse(value, out var result) ? result : defaultValue;
}
