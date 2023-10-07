using BurguerRoyale.Infrastructure.Context;
using BurguerRoyale.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BurguerRoyale.IOC.Configurations
{
	public static class ConfigureDatabase
	{
		public static void Register
		(
			IServiceCollection services,
			IConfiguration configuration
		)
		{
			services
				.AddEntityFrameworkSqlServer()
				.AddDbContext<ApplicationDbContext>(options =>
				{
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
				});

			services.AddScoped<ProductRepository>();
		}
	}
}
