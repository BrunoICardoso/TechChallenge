using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.Application.DTO
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            Images = Enumerable.Empty<ProductImage>();
            OrderProducts = Enumerable.Empty<OrderProduct>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }

        public IEnumerable<ProductImage> Images { get; set; }

        public IEnumerable<OrderProduct> OrderProducts { get; set; }
    }
}