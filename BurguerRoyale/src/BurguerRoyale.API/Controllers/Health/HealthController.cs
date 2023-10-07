using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BurguerRoyale.API.Controllers.Health;

/// <summary>
/// Health check controller
/// </summary>
[Route("health")]
public class HealthController : Controller
{
	private readonly HealthCheckService _healthCheckService;

	/// <summary>
	/// Constructor
	/// </summary>
	/// <param name="healthCheckService">HealthCheck Service from DI</param>
	public HealthController(HealthCheckService healthCheckService)
	{
		_healthCheckService = healthCheckService;
	}

	/// <summary>
	/// GetHealthCheck - Get health status of the application
	/// </summary>
	/// <remarks>Provides API Health indication</remarks>
	/// <response code="200">API is healthy</response>
	/// <response code="503">API is unhealthy or in degraded state</response>
	[HttpGet]
	[SwaggerResponse((int)HttpStatusCode.OK, "Api is healthy")]
	[SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Api is not healthy")]
	public async Task<IActionResult> GetHealthCheck(CancellationToken cancellationToken)
	{
		var report = await _healthCheckService.CheckHealthAsync(cancellationToken);

		return report.Status == HealthStatus.Healthy
			? Ok(report)
			: StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
	}
}
