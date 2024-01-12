using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IOrderService
	{
		Task<OrderDTO> CreateAsync(CreateOrderDTO orderDTO);

		Task<OrderDTO> GetOrderAsync(Guid id);

		Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus);

		Task UpdateOrderStatusAsync(Guid id, OrderStatus orderStatus);

		Task RemoveAsync(Guid id);

		Task<int> GenerateOrderNumber();
	}
}