using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.DTO
{
    public class RequestProductDTO
    {
        public string Name { get; set; } = string.Empty;

        public ProductCategory Category { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public List<ProductImageDTO>? Images { get; set; }
    }
}