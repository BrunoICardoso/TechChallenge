using FakePaymentService.Domain.Interface.Services;
using Flurl.Http;

namespace FakePaymentService.Infrastructure.Services
{
	public class NotificationService : INotificationService
	{
		public async Task NotifyPaymentAsync(string callbackUrl)
		{
			try
			{
				await callbackUrl.PostAsync();
			}
			catch (Exception)
			{
				return;
			}
		}
	}
}
