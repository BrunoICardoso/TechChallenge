using FakePaymentService.Domain.Interface.Repositories;
using FakePaymentService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentService.IOC.Configurations
{
	[ExcludeFromCodeCoverage]
	public static class ConfigureServices
	{
		public static void Register
		(
			IServiceCollection services
		)
		{
			#region Repositories

			services.AddScoped<IPaymentRepository, PaymentRepository>();

			#endregion Repositories
		}
	}
}
