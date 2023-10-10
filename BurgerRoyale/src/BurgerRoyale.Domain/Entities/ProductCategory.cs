namespace BurgerRoyale.Domain.Entities
{
	public class ProductCategory : Entity
	{
		public string Description { get; private set; }

		public virtual IEnumerable<Product> Products { get; set; }

		public ProductCategory(string description)
		{
			Description = description;
		}
	}
}