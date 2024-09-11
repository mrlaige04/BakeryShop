﻿using System.ComponentModel.DataAnnotations.Schema;
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
}