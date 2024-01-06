using BurgerRoyale.Application.ExternalServices.Payment.Interface;
using BurgerRoyale.Application.ExternalServices.Payment.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace BurgerRoyale.Application.ExternalServices.Payment.Services;

public class PaymentService : IPaymentService
{
    public readonly HttpClient _apiClient;
    private readonly IConfiguration _configuration;
    private const string createPaymentUrl = "api/payments";
    private readonly IActionContextAccessor _actionContextAccessor;

    public PaymentService(HttpClient apiClient, IConfiguration configuration, IActionContextAccessor actionContextAccessor)
    {
        _apiClient = apiClient;
        _configuration = configuration;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task SendAsync(Guid orderId, decimal price)
    {
        try
        {
            var url = _configuration["PaymentExternalApi:BaseUrl"] + createPaymentUrl;

            PaymentRequest paymentRequest = new PaymentRequest()
            {
                Amount = price,
                ClientIdentifier = orderId,
                CallbackUrl = $"{_actionContextAccessor.ActionContext.HttpContext.Request.Scheme}://{_actionContextAccessor.ActionContext.HttpContext.Request.Host}/api/order/{orderId}"
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(paymentRequest),
                Encoding.UTF8,
                "application/json");

            var response = await _apiClient.PostAsync(url, jsonContent);
            if (!response.IsSuccessStatusCode)
                throw new ExternalException("Falha ao solicitar pagamento.");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}