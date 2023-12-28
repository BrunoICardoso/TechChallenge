using FakePaymentMicroservice.Application.Services;
using FakePaymentMicroservice.Domain.Interface.Repositories;
using FakePaymentMicroservice.Domain.Interface.Services;
using FakePaymentMicroservice.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentMicroservice.IOC.Configurations
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


            #endregion Services

            #region Repositories

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            #endregion Repositories
        }
    }
}
