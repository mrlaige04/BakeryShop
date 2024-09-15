using BakeryShop.Application.Products;

namespace BakeryShop.Application.Users.Cart;
public record CartItemDto(ProductDto Product, double Quantity);