using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.IOC.Configurations
{
	public static class ConfigureServices
	{
		public static void Register
		(
			IServiceCollection services
		)
		{
			#region Services

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IProductService, ProductService>();

			#endregion Services

			#region Repositories

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();

			#endregion Repositories
		}
	}
}
