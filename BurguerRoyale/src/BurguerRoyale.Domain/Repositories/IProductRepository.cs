using BurguerRoyale.Domain.Entities;

namespace BurguerRoyale.Domain.Repositories
{
	public interface IProductRepository
	{
		Task<IEnumerable<Product>> GetListByCategoryAsync(string category);
	}
}
