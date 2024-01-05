namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IPaymentServiceIntegration
	{
		Task<Guid> CreateRequestPaymentAsync(decimal amount, Guid orderId);
	}
}