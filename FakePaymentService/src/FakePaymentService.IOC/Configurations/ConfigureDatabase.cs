using FakePaymentService.Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentService.IOC.Configurations
{
	[ExcludeFromCodeCoverage]
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
					options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
						  sqlServerOptionsAction: sqlOptions =>
						  {
							  sqlOptions.EnableRetryOnFailure(
								  maxRetryCount: 3,
								  maxRetryDelay: TimeSpan.FromSeconds(10),
								  errorNumbersToAdd: null);
						  });
				});
		}

		public static void RunMigrations(WebApplication app)
		{
			using var scope = app.Services.CreateScope();

			var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

			if (dbContext is not null && !dbContext.Database.CanConnect())
			{
				dbContext.Database.Migrate();
			}
		}
	}
}
