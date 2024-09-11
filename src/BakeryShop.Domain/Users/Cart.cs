using System.ComponentModel.DataAnnotations.Schema;
using BakeryShop.Domain.Abstractions;
using BakeryShop.Domain.Items;

namespace BakeryShop.Domain.Users;

public class Cart : Entity
{
    /*private readonly IList<BakeryItem> _items = [];

    [NotMapped] public IReadOnlyCollection<BakeryItem> Items => _items.AsReadOnly();

    public void AddItem(BakeryItem item)
    {
        _items
    }

    public void ClearCart() => _items.Clear();s*/
}