using BurgerRoyale.API.Controllers.Health;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NSubstitute;
using System.Net;
using Xunit;

namespace BurgerRoyale.UnitTests.API.Controllers.Health
{
	public class HealthControllerTests
	{
		private readonly HealthCheckService _healthCheckService;
		private readonly HealthController _controller;

		public HealthControllerTests()
		{
			_healthCheckService = Substitute.For<HealthCheckService>();

			_controller = new HealthController(_healthCheckService);
		}

		[Fact]
		public async Task GivenAnHealthyService_WhenGetHealthCheck_ThenShouldReturnOkStatus()
		{
			// arrange
			_healthCheckService
				.CheckHealthAsync(Arg.Any<CancellationToken>())
				.Returns(new HealthReport(
					new Dictionary<string, HealthReportEntry>(),
					HealthStatus.Healthy,
					TimeSpan.FromSeconds(1)
				));

			// act
			var response = await _controller.GetHealthCheck(CancellationToken.None) as ObjectResult;

			// assert
			response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
		}

		[Fact]
		public async Task GivenAnUnhealthyService_WhenGetHealthCheck_ThenShouldReturnServiceUnavailableStatus()
		{
			// arrange
			_healthCheckService
				.CheckHealthAsync(Arg.Any<CancellationToken>())
				.Returns(new HealthReport(
					new Dictionary<string, HealthReportEntry>(),
					HealthStatus.Unhealthy,
					TimeSpan.FromSeconds(1)
				));

			// act
			var response = await _controller.GetHealthCheck(CancellationToken.None) as ObjectResult;

			// assert
			response?.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);
		}
	}
}
