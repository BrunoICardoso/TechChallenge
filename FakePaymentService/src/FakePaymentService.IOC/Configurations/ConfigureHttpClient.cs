using FakePaymentService.Domain.Interface.Services;
using FakePaymentService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentService.IOC.Configurations
{
	[ExcludeFromCodeCoverage]
	public static class ConfigureHttpClient
	{
		public static void Register
		(
			IServiceCollection services
		)
		{
			services.AddHttpClient<INotificationService, NotificationService>();
		}
	}
}
