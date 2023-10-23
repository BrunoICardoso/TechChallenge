using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.ResponseDefault;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;

namespace BurgerRoyale.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch (Exception exception)
			{
				await HandleExceptionAsync(context, exception);
			}
		}

		[ExcludeFromCodeCoverage]
		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json; charset=utf-8";

			var statusCode = MapHttpStatusCode(exception);

			context.Response.StatusCode = (int)statusCode;

			var response = GetErrorResponse(exception, statusCode);

			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}

		public ReturnAPI GetErrorResponse(Exception exception, HttpStatusCode statusCode)
		{
			return new ReturnAPI(statusCode)
			{
				Message = exception.Message,
				Exception = exception
			};
		}

		public HttpStatusCode MapHttpStatusCode(Exception exception) => exception switch
		{
			var e when e is DomainException => HttpStatusCode.BadRequest,
			var e when e is NotFoundException => HttpStatusCode.NotFound,
			_ => HttpStatusCode.InternalServerError
		};
	}
}
