using BurgerRoyale.API.Middleware;
using BurgerRoyale.Domain.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Net;
using Xunit;

namespace BurgerRoyale.UnitTests.API.Middleware
{
	public class ExceptionMiddlewareTests
	{
		private readonly Mock<RequestDelegate> _requestDelegate;
		private readonly Mock<HttpContext> _context;

		private readonly ExceptionMiddleware _middleware;

		public ExceptionMiddlewareTests()
		{
			_requestDelegate = new Mock<RequestDelegate>();
			_context = new Mock<HttpContext>();
			_middleware = new ExceptionMiddleware(_requestDelegate.Object);
		}

		[Fact]
		public async Task GivenRequest_WhenInvoke_ThenShouldNotThrowException()
		{
			// arrange
			_requestDelegate
				.Setup(x => x.Invoke(It.IsAny<HttpContext>()))
				.Returns(Task.CompletedTask);

			// act
			Func<Task> task = async () => await _middleware.Invoke(_context.Object);

			// assert
			await task.Should().NotThrowAsync<Exception>();
		}

		[Fact]
		public async Task GivenRequest_WhenThrowExceptionOnInvoke_ThenShouldThrowException()
		{
			// arrange
			_requestDelegate
				.Setup(x => x.Invoke(It.IsAny<HttpContext>()))
				.ThrowsAsync(new Exception("Exception message"));

			// act
			Func<Task> task = async () => await _middleware.Invoke(_context.Object);

			// assert
			await task.Should().ThrowAsync<Exception>();
		}

		[Fact]
		public void GivenException_WhenGetResponse_ThenReturnApiResponse()
		{
			// arrange
			var exception = new Exception("Exception message");

			// act
			var response = _middleware.GetErrorResponse(exception, HttpStatusCode.BadRequest);

			// assert
			response.Message.Should().Be("Exception message");
			response.Exception.Should().Be(exception);
		}

		[Fact]
		public void GivenDomainException_WhenMapHttpStatusCode_ThenShouldReturnBadRequestStatusCode()
		{
			// arrange
			var exception = new DomainException();

			// act
			var response = _middleware.MapHttpStatusCode(exception);

			// assert
			response.Should().Be(HttpStatusCode.BadRequest);
		}

		[Fact]
		public void GivenNotFoundException_WhenMapHttpStatusCode_ThenShouldReturnNotFoundStatusCode()
		{
			// arrange
			var exception = new NotFoundException();

			// act
			var response = _middleware.MapHttpStatusCode(exception);

			// assert
			response.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public void GivenGenericException_WhenMapHttpStatusCode_ThenShouldReturnInternalServerErrorStatusCode()
		{
			// arrange
			var exception = new Exception();

			// act
			var response = _middleware.MapHttpStatusCode(exception);

			// assert
			response.Should().Be(HttpStatusCode.InternalServerError);
		}
	}
}
