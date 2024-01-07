using BurgerRoyale.Domain.Interface.Repositories;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace BurgerRoyale.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    public readonly HttpClient _apiClient;
    
    private readonly IConfiguration _configuration;
    
    private readonly IActionContextAccessor _actionContextAccessor;

    private const string createPaymentUrl = "api/payments";
    
    public PaymentRepository(
        HttpClient apiClient,
        IConfiguration configuration,
        IActionContextAccessor actionContextAccessor)
    {
        _apiClient = apiClient;
        _configuration = configuration;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task SendAsync(Guid orderId, decimal price)
    {
        try
        {
            string url = _configuration["PaymentExternalApi:BaseUrl"] + createPaymentUrl;

            var paymentRequest = new
            {
                Amount = price,
                ClientIdentifier = orderId,
                CallbackUrl = GetCallbackUrl(orderId)
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(paymentRequest),
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response = await _apiClient.PostAsync(url, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new ExternalException("Falha ao solicitar pagamento.");
        }
        catch
        {
            throw;
        }
    }

    private string GetCallbackUrl(Guid orderId)
    {
        return $"{_actionContextAccessor.ActionContext!.HttpContext.Request.Scheme}://{_actionContextAccessor.ActionContext.HttpContext.Request.Host}/api/order/{orderId}";
    }
}