using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Orders;
public class DeliveryInfo : Entity
{
    public Guid OrderId { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public DateTime? DeliveryDate { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
}

public enum DeliveryStatus
{
    Pending,
    InProgress,
    Delivered,
    Cancelled
}
