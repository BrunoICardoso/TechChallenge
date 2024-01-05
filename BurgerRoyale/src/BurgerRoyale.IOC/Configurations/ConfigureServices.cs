using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Infrastructure.Integrations;
using BurgerRoyale.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.IOC.Configurations
{
	[ExcludeFromCodeCoverage]
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
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IPaymentServiceIntegration, PaymentServiceIntegration>();

			#endregion Services

			#region Repositories

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductImageRepository, ProductImageRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();

			#endregion Repositories
		}
	}
}
