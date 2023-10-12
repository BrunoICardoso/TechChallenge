using BurgerRoyale.Domain.Base;

namespace BurgerRoyale.Domain.Entities
{
	public class Product : Entity
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }
		public Guid CategoryId { get; private set; }
		public virtual ProductCategory Category { get; set; }
		public virtual IEnumerable<ProductImage> Images { get; set; }
		public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }

		public Product(string name, string description, decimal price, Guid categoryId)
		{
			Name = name;
			Description = description;
			Price = price;
			CategoryId = categoryId;
			ValidateEntity();
		}

        public void ValidateEntity()
        {
			AssertionConcern.AssertArgumentNotEmpty(Name, "The name is required!");
        }
    }
}