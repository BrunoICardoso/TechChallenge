using BurgerRoyale.Domain.Base;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public ProductCategory Category { get; set; }

    public virtual IEnumerable<ProductImage> Images { get; set; }
    public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }

    public Product(string name, string description, decimal price, ProductCategory category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ValidateEntity();
    }

    public Product(Guid id, string name, string description, decimal price, ProductCategory category) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ValidateEntity();
    }

    public void SetDetails(string name, string description, decimal price, ProductCategory category)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }

    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Name, "The name is required!");
        AssertionConcern.AssertArgumentHasValidPrice(Price, "The price is invalid!");
    }
}