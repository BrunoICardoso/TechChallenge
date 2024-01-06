using BurgerRoyale.Application.ExternalServices.Payment.Interface;
using BurgerRoyale.Application.ExternalServices.Payment.Services;
using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

            #endregion Services

            #region External Services
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddHttpClient();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            #endregion

            #region Repositories

            services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductImageRepository, ProductImageRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();

			#endregion Repositories
		}
	}
}
