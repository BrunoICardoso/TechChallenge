using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.RepositoriesStandard;

namespace BurgerRoyale.Domain.Repositories
{
	public interface IProductRepository : IDomainRepository<Product>
	{
		Task<IEnumerable<Product>> GetListByCategoryAsync(string category);
	}
}
