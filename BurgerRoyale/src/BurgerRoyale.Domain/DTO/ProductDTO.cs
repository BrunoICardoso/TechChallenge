using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.DTO
{
	public class ProductDTO
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public ProductCategory Category { get; set; }

		public string Description { get; set; } = string.Empty;

		public decimal Price { get; set; }
	}
}