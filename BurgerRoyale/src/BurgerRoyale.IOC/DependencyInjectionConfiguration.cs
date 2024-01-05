using BurgerRoyale.IOC.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.IOC
{
	[ExcludeFromCodeCoverage]
	public static class DependencyInjectionConfiguration
	{
		public static void Register
		(
			IServiceCollection services,
			IConfiguration configuration
		)
		{
			ConfigureDatabase.Register(services, configuration);
			ConfigureHealthChecks.Register(services);
			ConfigureServices.Register(services);
			ConfigureHttpClient.Register(services, configuration);
		}
	}
}