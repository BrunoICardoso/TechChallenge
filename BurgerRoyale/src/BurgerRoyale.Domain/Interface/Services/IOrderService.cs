using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Interface.Services
{
    public interface IOrderService
    {
        Task CreateAsync(CreateOrderDTO orderDTO);

        Task<OrderDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDTO>> GetOrdersAsync(OrderStatus? orderStatus);

        Task<OrderDTO> UpdateAsync(Guid id, OrderDTO orderDTO);

        Task<OrderDTO> RemoveAsync(Guid id);
    }
}