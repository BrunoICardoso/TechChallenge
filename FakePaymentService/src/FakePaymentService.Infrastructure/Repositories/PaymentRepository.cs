using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Interface.Repositories;
using FakePaymentService.Infrastructure.Context;
using FakePaymentService.Infrastructure.RepositoriesStandard;

namespace FakePaymentService.Infrastructure.Repositories
{
	public class PaymentRepository : DomainRepository<Payment>, IPaymentRepository
	{
		public PaymentRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}