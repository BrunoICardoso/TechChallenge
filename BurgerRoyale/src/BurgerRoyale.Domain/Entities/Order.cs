namespace BurgerRoyale.Domain.Entities
{
	public class Order : Entity
	{
		public Guid UserId { get; set; }
		public Guid StatusId { get; set; }
		public DateTime OrderTime { get; set; }
		public DateTime? CloseTime { get; set; }

		public virtual OrderStatus Status { get; private set; }
		public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
	}
}