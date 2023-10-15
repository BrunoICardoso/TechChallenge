using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
    public class OrderRepository : DomainRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
