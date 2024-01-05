using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BurgerRoyale.Infrastructure.Integrations
{
	public class PaymentServiceIntegration : IPaymentServiceIntegration
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly HttpClient _httpClient;

		public PaymentServiceIntegration
		(
			IHttpContextAccessor httpContextAccessor,
			HttpClient httpClient
		)
		{
			_httpContextAccessor = httpContextAccessor;
			_httpClient = httpClient;
		}

		public async Task<Guid> CreateRequestPaymentAsync(decimal amount, Guid orderId)
		{
			var request = _httpContextAccessor.HttpContext?.Request;

			string callbackUrl = $"{request.Scheme}://{request.Host}/api/Order/{orderId}:approve";

			var paymentRequest = new CreatePaymentDto(
				amount,
				orderId,
				callbackUrl
			);

			var response = await _httpClient.PostAsync(
				"/api/payments",
				JsonContent.Create(paymentRequest)
			);

			response.EnsureSuccessStatusCode();

			var stringContent = await response.Content.ReadAsStringAsync();

			var payment = JsonSerializer.Deserialize<PaymentDto>(
				stringContent,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				}
			);

			return payment?.PaymentId ?? throw new Exception("Error requesting payment");
		}
	}
}
