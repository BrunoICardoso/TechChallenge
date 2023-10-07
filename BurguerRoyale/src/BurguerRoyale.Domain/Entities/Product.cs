namespace BurguerRoyale.Domain.Entities
{
	public class Product : Entity
	{
		public string Name { get; private set; }
		public string Category { get; private set; }
		public decimal Price { get; private set; }

		public Product(string name, string category, decimal price)
		{
			Name = name;
			Category = category;
			Price = price;
		}
	}
}
