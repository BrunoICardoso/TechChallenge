using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Helpers;

namespace BurgerRoyale.Domain.DTO
{
	public class OrderDTO
	{
		public Guid OrderId { get; set; }
		public Guid UserId { get; set; }
		public string Status { get; set; }
		public DateTime OrderTime { get; set; }
		public DateTime? CloseTime { get; set; }
		public int OrderNumber { get; set; }
		public decimal TotalPrice { get; set; }
		public IEnumerable<OrderProductDTO> OrderProducts { get; set; } = Enumerable.Empty<OrderProductDTO>();

		public OrderDTO(Order order)
		{
			OrderId = order.Id;
			Status = order.Status.GetDescription();
			UserId = order.UserId;
			OrderTime = order.OrderTime;
			CloseTime = order.CloseTime;
			OrderProducts = order.OrderProducts.Select(orderProduct => new OrderProductDTO(orderProduct));
			OrderNumber = order.OrderNumber;
			TotalPrice = order.TotalPrice;
		}
	}
}
