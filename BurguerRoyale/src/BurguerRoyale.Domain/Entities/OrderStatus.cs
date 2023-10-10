namespace BurguerRoyale.Domain.Entities
{
	public class OrderStatus : Entity
	{
		public string Description { get; private set; }

		public virtual IEnumerable<Order> Orders { get; set; }

		public OrderStatus(string description)
		{
			Description = description;
		}
	}
}