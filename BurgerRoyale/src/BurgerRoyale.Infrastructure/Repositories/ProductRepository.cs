using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
	public class ProductRepository : DomainRepository<Product>, IProductRepository
	{
		public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}

		public Task<IEnumerable<Product>> GetListByCategoryAsync(string category)
		{
			throw new NotImplementedException();
		}
	}
}
