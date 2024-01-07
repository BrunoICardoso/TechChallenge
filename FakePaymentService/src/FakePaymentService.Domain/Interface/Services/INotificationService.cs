namespace FakePaymentService.Domain.Interface.Services
{
	public interface INotificationService
	{
		Task NotifyPaymentAsync(string callbackUrl);
	}
}
