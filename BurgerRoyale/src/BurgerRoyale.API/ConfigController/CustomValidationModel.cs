using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BurgerRoyale.API.ConfigController
{
	public class CustomValidationModel : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{

				var errorList = context.ModelState.ToDictionary(
														kvp => kvp.Key,
														kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
													);

				context.Result = new ObjectResult(new ReturnAPI
				{
					StatusCode = HttpStatusCode.BadRequest,
					Message = "Ocorreu um erro de validação.",
					ModelState = errorList
				})

				{ StatusCode = (int)HttpStatusCode.BadRequest };

			}
		}
	}
}

