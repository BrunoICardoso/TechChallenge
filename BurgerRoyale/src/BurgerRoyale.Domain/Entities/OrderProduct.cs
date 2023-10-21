namespace BurgerRoyale.Domain.Entities
{
	public class OrderProduct
	{
		public Guid OrderId { get; private set; }
		public Guid ProductId { get; private set; }
		public decimal ProductPrice { get; private set; }
		public int Quantity { get; private set; }

		public virtual Order Order { get; private set; }
		public virtual Product Product { get; private set; }

		public OrderProduct(Guid orderId, Guid productId, decimal productPrice, int quantity)
		{
			OrderId = orderId;
			ProductId = productId;
			ProductPrice = productPrice;
			Quantity = quantity;
		}

		public OrderProduct(Guid orderId, Guid productId, decimal productPrice, int quantity, Product product)
		{
			OrderId = orderId;
			ProductId = productId;
			ProductPrice = productPrice;
			Quantity = quantity;
			Product = product;
		}
	}
}