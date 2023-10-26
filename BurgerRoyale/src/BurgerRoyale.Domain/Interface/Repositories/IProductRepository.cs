using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.RepositoriesStandard;

namespace BurgerRoyale.Domain.Interface.Repositories
{
    public interface IProductRepository : IDomainRepository<Product>
	{
		Task <IEnumerable<Product>> GetAll();
		Task <IEnumerable<Product>> GetAllByCategory(ProductCategory category);
		Task <Product> GetProductById(Guid id);
	}
}