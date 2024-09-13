using BakeryShop.Domain.Abstractions;

namespace BakeryShop.Domain.Products;
public class Currency : Entity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;

    private Currency() {} 

    private Currency(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public static Currency Create(string code, string name) => new(code, name);
}
