using BurgerRoyale.Application.Interface.Services;
using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerRoyale.IOC
{
    public static class InjectionConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region Product
            
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            #endregion

            #region User

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            #endregion
        }
    }
}