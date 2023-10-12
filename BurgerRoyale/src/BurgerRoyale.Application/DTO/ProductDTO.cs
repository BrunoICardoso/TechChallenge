namespace BurgerRoyale.Application.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}