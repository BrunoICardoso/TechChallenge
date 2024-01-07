using FakePaymentService.Domain.Entities;
using FakePaymentService.Domain.Interface.RepositoriesStandard;

namespace FakePaymentService.Domain.Interface.Repositories
{
	public interface IPaymentRepository : IDomainRepository<Payment>
	{
	}
}