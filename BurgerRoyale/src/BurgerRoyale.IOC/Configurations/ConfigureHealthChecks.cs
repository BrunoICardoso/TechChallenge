using BurgerRoyale.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.IOC.Configurations
{
	[ExcludeFromCodeCoverage]
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
