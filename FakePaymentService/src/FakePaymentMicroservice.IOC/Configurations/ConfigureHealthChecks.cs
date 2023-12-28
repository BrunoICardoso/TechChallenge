using FakePaymentMicroservice.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentMicroservice.IOC.Configurations
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
