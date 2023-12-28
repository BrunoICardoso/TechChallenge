using FakePaymentMicroservice.Domain.Entities;
using FakePaymentMicroservice.Domain.Interface.Repositories;
using FakePaymentMicroservice.Infrastructure.Context;
using FakePaymentMicroservice.Infrastructure.RepositoriesStandard;

namespace FakePaymentMicroservice.Infrastructure.Repositories
{
    public class ProductImageRepository : DomainRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}