using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Interface.Services
{
    public interface IOrderService
    {
        Task CreateAsync(CreateOrderDTO orderDTO);

        Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus);

        Task UpdateOrderStatusAsync(Guid id, OrderStatus orderStatus);

        Task RemoveAsync(Guid id);
    }
}