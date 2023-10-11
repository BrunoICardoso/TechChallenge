using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.RepositoriesStandard;

namespace BurgerRoyale.Domain.Interface.Repositories
{
    public interface IProductRepository : IDomainRepository<Product>
    {
        Task<IEnumerable<Product>> GetListByCategoryAsync(string category);
    }
}
