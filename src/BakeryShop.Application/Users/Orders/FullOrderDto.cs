using BakeryShop.Domain.Orders;

namespace BakeryShop.Application.Users.Orders;
public class FullOrderDto
{
    public Guid Id { get; set; }
    public OrderStatus Status { get; set; }
    public DeliveryInfo DeliveryInfo { get; set; } = null!;
    public IEnumerable<OrderItemDto> Items { get; set; } = null!;
}
