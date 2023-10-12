namespace BurgerRoyale.Application.DTO
{
    public class AddProductRequestDTO
    {
        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}