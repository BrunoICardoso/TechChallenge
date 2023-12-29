using FakePaymentService.Application.Services;
using FakePaymentService.Domain.Interface.Repositories;
using FakePaymentService.Domain.Interface.Services;
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
			#region Services

			services.AddScoped<IPaymentService, PaymentService>();

			#endregion Services

			#region Repositories

			services.AddScoped<IPaymentRepository, PaymentRepository>();

			#endregion Repositories
		}
	}
}
