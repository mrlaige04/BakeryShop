using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Orders;
using Microsoft.AspNetCore.Identity;

namespace BakeryShop.Domain.Users;

public class User : IdentityUser<Guid>, IEntity<Guid>
{
    public Cart Cart { get; set; } = null!;
    public Guid CartId { get; set; }

    private readonly IList<Order> _orders = [];

    [NotMapped] public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    public static User Create(string email) => new() { 
        Email = email,
        UserName = email ,
        Cart = new()
    };

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public void ClearCart()
    {
        Cart?.ClearCart();
    }
}