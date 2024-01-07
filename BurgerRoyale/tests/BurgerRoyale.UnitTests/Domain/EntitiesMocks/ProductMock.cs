using Bogus;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.UnitTests.Domain.EntitiesMocks
{
    public static class ProductMock
    {
        public static Product Get
        (
            Guid? id = null,
            string? name = null,
            string? description = null,
            decimal? price = null,
            ProductCategory? category = null
        )
        {
            return new Faker<Product>()
                .CustomInstantiator(faker => new Product(
                    id ?? Guid.NewGuid(),
                    name ?? faker.Commerce.ProductName(),
                    description ?? faker.Commerce.ProductDescription(),
                    price ?? decimal.Parse(faker.Commerce.Price()),
                    category ?? faker.PickRandom<ProductCategory>()
                ));
        }
    }
}
