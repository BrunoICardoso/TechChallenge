using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
    public class ProductImageRepository : DomainRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}