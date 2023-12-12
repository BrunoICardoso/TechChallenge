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
		public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}

		public async Task<IEnumerable<Order>> GetOrders(OrderStatus? orderStatus)
		{
			var query = _context.Orders.Include(x => x.OrderProducts).ThenInclude(x => x.Product).Where(x => x.Status != OrderStatus.Finalizado);
			if (orderStatus == null)
                return await query.OrderByDescending(x => x.Status).ThenBy(x => x.OrderTime).ToListAsync();

            return await query.Where(x => x.Status == orderStatus).OrderByDescending(x => x.Status).ThenBy(x => x.OrderTime).ToListAsync();
        }
	}
}
