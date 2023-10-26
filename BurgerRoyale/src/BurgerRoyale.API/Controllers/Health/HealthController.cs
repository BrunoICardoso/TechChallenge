using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace BurgerRoyale.API.Controllers.Health;


[Route("health")]
public class HealthController : Controller
{
	private readonly HealthCheckService _healthCheckService;

	public HealthController(HealthCheckService healthCheckService)
	{
		_healthCheckService = healthCheckService;
	}

	[HttpGet]
    [SwaggerOperation(Summary = "GetHealthCheck - Get health status of the application", Description = "Provides API Health indication.")]
    [ProducesResponseType(typeof(HealthReport), StatusCodes.Status200OK)]
	[SwaggerResponse((int)HttpStatusCode.OK, "Api is healthy")]
    [ProducesResponseType(typeof(ObjectResult), StatusCodes.Status503ServiceUnavailable)]
	[SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Api is not healthy")]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetHealthCheck(CancellationToken cancellationToken)
	{
		var report = await _healthCheckService.CheckHealthAsync(cancellationToken);

		return report.Status == HealthStatus.Healthy
			? Ok(report)
			: StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
	}
}