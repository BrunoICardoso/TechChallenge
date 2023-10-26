using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;
using Microsoft.EntityFrameworkCore;

namespace BurgerRoyale.Infrastructure.Repositories
{
    public class ProductRepository : DomainRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(x => x.Images).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByCategory(ProductCategory category)
        {
            return await _context.Products.Include(x => x.Images).Where(x => x.Category == category).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.Include(x => x.Images).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
