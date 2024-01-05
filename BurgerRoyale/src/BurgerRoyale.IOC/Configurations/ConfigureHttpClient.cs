using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Infrastructure.Integrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.IOC.Configurations;

[ExcludeFromCodeCoverage]
public static class ConfigureHttpClient
{
	public static void Register
	(
		IServiceCollection services,
		IConfiguration configuration
	)
	{
		services.AddHttpClient<IPaymentServiceIntegration, PaymentServiceIntegration>(config =>
		{
			config.BaseAddress = new Uri(configuration.GetSection("PaymentService").GetValue<string>("BaseUrl") ?? string.Empty);
		});
	}
}
