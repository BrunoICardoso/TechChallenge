using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;
using Microsoft.EntityFrameworkCore;

namespace BurgerRoyale.Infrastructure.Repositories
{
    public class OrderRepository : DomainRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Order>> GetOrders(OrderStatus? orderStatus)
        {
            var query = _dbContext.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product);
            if (orderStatus == null)
                return await query.ToListAsync();

            return await query.Where(x => x.Status == orderStatus).ToListAsync();
        }
    }
}
