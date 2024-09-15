using Ardalis.Result;
using BakeryShop.Application.Common.Abstractions;
using BakeryShop.Application.Common.Extensions;
using BakeryShop.Application.Common.Models;
using BakeryShop.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BakeryShop.Application.Products.SearchProducts;
internal sealed class SearchProductsQueryHandler(
    IProductRepository productRepository,
    ILogger<SearchProductsQueryHandler> logger
    )
    : IQueryHandler<SearchProductsQuery, PaginatedList<ProductDto>>
{
    public async Task<Result<PaginatedList<ProductDto>>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("SearchProductsQuery: Started.");

        var products = productRepository.Source;

        products = ApplyFiltering(products, request);

        var pageNumber = ParseIntOrDefault(request.PageNumber?.ToString(), 1);
        var pageSize = ParseIntOrDefault(request.PageSize?.ToString(), 10);

        if (pageNumber < 0)
        {
            logger.LogInformation("SearchProductsQuery: Error. Invalid page number");
            return Result.Error("PageNumber cannot be a negative number");
        }
            
        if (pageSize < 0)
        {
            logger.LogInformation("SearchProductsQuery: Error. Invalid page number");
            return Result.Error("PageSize cannot be a negative number");
        }
            
        var result = await products
            .Select(product => (ProductDto)product)
            .PaginatedListAsync(pageNumber, pageSize);

        logger.LogInformation("SearchProductsQuery: Success.");

        return result;
    }

    private static IQueryable<Product> ApplyFiltering(IQueryable<Product> products, SearchProductsQuery request)
    {
        if (!string.IsNullOrEmpty(request.Query))
        {
            products = products.Where(p => EF.Functions.Like(p.Title, $"%{request.Query}%"));
        }

        if (decimal.TryParse(request.PriceFrom?.ToString(), out decimal priceFrom))
            products = products.Where(p => p.Price >= priceFrom);

        if (decimal.TryParse(request.PriceTo?.ToString(), out decimal priceTo))
            products = products.Where(p => p.Price <= priceTo);

        return products;
    }

    private static int ParseIntOrDefault(string? value, int defaultValue) =>
        int.TryParse(value, out var result) ? result : defaultValue;
}
