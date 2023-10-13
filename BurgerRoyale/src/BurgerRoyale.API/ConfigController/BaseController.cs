using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace BurgerRoyale.API.ConfigController
{
	public class BaseController : ControllerBase
	{
		public ActionResult<T> StatusCode<T>([ActionResultObjectValue] T returnAPI) where T : ReturnAPI
		{
			var statusCode = GetStatus(returnAPI);

			if (statusCode != null)
			{
				var result = new ObjectResult(returnAPI) { StatusCode = (int)statusCode };

				return result;
			}

			throw new Exception("Não foi informado StatusCode");
		}

		public IActionResult IStatusCode<T>([ActionResultObjectValue] T returnAPI) where T : ReturnAPI
		{
			var statusCode = GetStatus(returnAPI);

			if (statusCode != null)
			{
				var result = new ObjectResult(returnAPI) { StatusCode = (int)statusCode };

				return result;
			}

			throw new Exception("Não foi informado StatusCode");
		}

		private HttpStatusCode? GetStatus(object obj)
		{
			var type = obj.GetType();

			if (obj.GetType().Name.StartsWith("ReturnAPI"))
			{
				return (HttpStatusCode)obj.GetType().GetProperty("StatusCode").GetValue(obj);
			}
			else
			{
				return null;
			}
		}
	}
}
