using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.RepositoriesStandard;

namespace BurgerRoyale.Domain.Interface.Repositories
{
    public interface IOrderRepository : IDomainRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrders(OrderStatus? orderStatus);
    }
}