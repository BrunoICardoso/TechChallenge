using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BurgerRoyale.Infrastructure.Integrations;

public class PaymentServiceIntegration : IPaymentServiceIntegration
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient;

    private const string REQUEST_PAYMENT_URL = "api/payments";

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

        string callbackUrl = $"{request.Scheme}://{request.Host}/api/Order/{orderId}/approve";

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