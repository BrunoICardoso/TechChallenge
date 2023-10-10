using BurgerRoyale.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.IOC.Configurations
{
	public static class ConfigureHealthChecks
	{
		public static void Register
		(
			IServiceCollection services
		)
		{
			services
				.AddHealthChecks()
				.AddDbContextCheck<ApplicationDbContext>();
		}
	}
}
