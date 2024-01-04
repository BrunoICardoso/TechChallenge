using FakePaymentService.Domain.Interface.Services;

namespace FakePaymentService.Infrastructure.Services
{
	public class NotificationService : INotificationService
	{
		private readonly HttpClient _httpClient;

		public NotificationService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task NotifyPaymentAsync(string callbackUrl)
		{
			await _httpClient.PostAsync(callbackUrl, null);
		}
	}
}
