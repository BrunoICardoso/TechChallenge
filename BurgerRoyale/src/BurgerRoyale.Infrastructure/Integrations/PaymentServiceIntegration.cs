using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BurgerRoyale.Infrastructure.Integrations;

public class PaymentServiceIntegration : IPaymentServiceIntegration
{
	private readonly IConfiguration _configuration;
	private readonly HttpClient _httpClient;

	private const string REQUEST_PAYMENT_URL = "api/payments";

	public PaymentServiceIntegration
	(
		IConfiguration configuration,
		HttpClient httpClient
	)
	{
		_configuration = configuration;
		_httpClient = httpClient;
	}

	public async Task<Guid> CreateRequestPaymentAsync(decimal amount, Guid orderId)
	{
		var callbackBaseUrl = _configuration.GetSection("BurgerRoyale").GetValue<string>("BaseUrl");

		string callbackUrl = $"{callbackBaseUrl}/api/Order/{orderId}/approve";

		var paymentRequest = new CreatePaymentDto(
			amount,
			orderId,
			callbackUrl
		);

		var response = await _httpClient.PostAsync(
			REQUEST_PAYMENT_URL,
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

		return payment?.PaymentId ?? throw new ExternalException("Error requesting payment");
	}
}