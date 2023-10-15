using BurgerRoyale.Application.Models;
using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
    public interface IOrderService
    {
        Task CreateAsync(OrderDTO orderDTO);

        Task<OrderDTO> GetByIdAsync(Guid id);

        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

        Task<OrderDTO> UpdateAsync(Guid id, OrderDTO orderDTO);

        Task<OrderDTO> RemoveAsync(Guid id);
    }
}