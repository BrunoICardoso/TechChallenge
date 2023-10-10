namespace BurguerRoyale.Domain.Entities
{
	public class Order : Entity
	{
		public Guid UserId { get; private set; }
		public Guid StatusId { get; private set; }
		public DateTime OrderTime { get; private set; }
		public DateTime? CloseTime { get; private set; }

		public virtual OrderStatus Status { get; private set; }
		public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
	}
}