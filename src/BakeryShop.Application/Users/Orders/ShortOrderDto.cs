using BakeryShop.Domain.Orders;

namespace BakeryShop.Application.Users.Orders;
public class ShortOrderDto
{
    public OrderStatus Status { get; set; }
    public DeliveryInfo DeliveryInfo { get; set; } = null!;
}
