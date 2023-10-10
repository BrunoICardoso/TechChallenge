using BurguerRoyale.Domain.Entities;
using BurguerRoyale.Domain.RepositoriesStandard;

namespace BurguerRoyale.Domain.Repositories
{
	public interface IProductRepository : IDomainRepository<Product>
	{
		Task<IEnumerable<Product>> GetListByCategoryAsync(string category);
	}
}
