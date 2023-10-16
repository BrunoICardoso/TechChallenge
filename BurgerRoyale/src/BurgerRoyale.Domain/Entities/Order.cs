using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities
{
	public class Order : Entity
	{
		public Guid UserId { get; set; }
		public DateTime OrderTime { get; set; }
		public DateTime? CloseTime { get; set; }
		public OrderStatus Status { get; set; }

		public virtual IEnumerable<OrderProduct> OrderProducts { get; set; }
	}
}