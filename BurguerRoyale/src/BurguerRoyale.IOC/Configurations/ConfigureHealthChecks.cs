using BurguerRoyale.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace BurguerRoyale.IOC.Configurations
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
