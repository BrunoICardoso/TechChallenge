using BurguerRoyale.Domain.Entities;
using BurguerRoyale.Domain.Repositories;
using BurguerRoyale.Infrastructure.Context;
using BurguerRoyale.Infrastructure.RepositoriesStandard;

namespace BurguerRoyale.Infrastructure.Repositories
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
