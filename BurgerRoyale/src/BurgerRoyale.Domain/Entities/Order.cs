using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities
{
	public class Order : Entity
	{
		public Guid UserId { get; private set; }
		public DateTime OrderTime { get; private set; }
		public DateTime? CloseTime { get; private set; }
		public OrderStatus Status { get; private set; }

		public virtual List<OrderProduct> OrderProducts { get; private set; } = new List<OrderProduct>();

        public Order(Guid userId)
        {
			UserId = userId;
			OrderTime = DateTime.Now;
			Status = OrderStatus.Recebido;
        }

		public void AddProduct(OrderProduct orderProduct)
		{
			OrderProducts.Add(orderProduct);
		}

		public void SetStatus(OrderStatus status)
		{
			Status = status;
		}
    }
}