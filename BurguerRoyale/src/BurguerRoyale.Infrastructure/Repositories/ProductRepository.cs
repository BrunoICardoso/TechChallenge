using BurguerRoyale.Domain.Entities;
using BurguerRoyale.Domain.Repositories;
using BurguerRoyale.Infrastructure.Context;

namespace BurguerRoyale.Infrastructure.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly ApplicationDbContext _context;

		public ProductRepository(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public Task<IEnumerable<Product>> GetListByCategoryAsync(string category)
		{
			throw new NotImplementedException();
		}
	}
}
