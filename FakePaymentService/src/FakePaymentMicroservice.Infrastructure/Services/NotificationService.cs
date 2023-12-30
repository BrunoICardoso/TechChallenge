using FakePaymentService.Domain.Interface.Services;
using Flurl.Http;

namespace FakePaymentService.Infrastructure.Services
{
	public class NotificationService : INotificationService
	{
		public async Task NotifyPaymentAsync(string callbackUrl)
		{
			await callbackUrl.PostAsync();
		}
	}
}
