using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Products;

public class Information : Entity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}