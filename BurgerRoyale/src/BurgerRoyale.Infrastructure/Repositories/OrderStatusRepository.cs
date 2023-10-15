using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
    public class OrderStatusRepository : DomainRepository<OrderStatus>, IOrderStatusRepository
    {
        public OrderStatusRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
