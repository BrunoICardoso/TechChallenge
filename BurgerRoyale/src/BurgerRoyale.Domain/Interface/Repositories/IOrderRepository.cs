using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.RepositoriesStandard;

namespace BurgerRoyale.Domain.Interface.Repositories
{
    public interface IOrderRepository : IDomainRepository<Order>
    {
    }
}