using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
	public class ProductRepository : DomainRepository<Product>, IProductRepository
	{
		public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}
