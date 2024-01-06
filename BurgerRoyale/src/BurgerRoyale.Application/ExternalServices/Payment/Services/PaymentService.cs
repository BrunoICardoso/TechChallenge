using BurgerRoyale.Application.ExternalServices.Payment.Interface;
using BurgerRoyale.Application.ExternalServices.Payment.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BurgerRoyale.Application.ExternalServices.Payment.Services;

public class PaymentService : IPaymentService
{
    public readonly HttpClient _apiClient;
    private readonly IConfiguration _configuration;
    private const string createPaymentUrl = "api/payments";

    public PaymentService(HttpClient apiClient, IConfiguration configuration)
    {
        _apiClient = apiClient;
        _configuration = configuration;
    }

    public async Task SendAsync(Guid orderId, decimal price)
    {
        var url = _configuration["PaymentExternalApi:BaseUrl"] + createPaymentUrl;
        PaymentRequest paymentRequest = new PaymentRequest()
        {
            Amount = price,
            ClientIdentifier = orderId,
            CallbackUrl = ""
        };

        var paymentRequestJson = JsonSerializer.Serialize(paymentRequest);
        HttpContent content = new StringContent(paymentRequestJson);
        await _apiClient.PostAsync(url, content);
    }
}