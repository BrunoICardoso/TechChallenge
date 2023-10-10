namespace BurgerRoyale.Domain.Entities
{
	public class ProductImage : Entity
	{
		public string Title { get; private set; }
		public string Url { get; private set; }
		public Guid ProductId { get; private set; }
		public virtual Product Product { get; set; }

		public ProductImage(string title, string url, Guid productId)
		{
			Title = title;
			Url = url;
			ProductId = productId;
		}
	}
}