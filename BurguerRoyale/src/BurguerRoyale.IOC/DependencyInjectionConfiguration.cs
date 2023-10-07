using BurguerRoyale.IOC.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BurguerRoyale.IOC
{
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
		}
	}
}