using BurgerRoyale.API.Controllers.Login;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using BurgerRoyale.UnitTests.Domain.EntitiesMocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace BurgerRoyale.UnitTests.API.Controllers.Login
{
    public class LoginControllerTests
	{
		private readonly Mock<IUserService> _userService;
		private readonly LoginController _loginController;

		public LoginControllerTests()
		{
			_userService = new Mock<IUserService>();

			_loginController = new LoginController(_userService.Object);
		}

		[Fact]
		public async Task GivenLoginRequest_WhenLoginByCpf_ThenShouldReturn()
		{
			// arrange
			var request = new LoginDTO { Cpf = "123.456.789-10" };

			var user = UserMock.Get(request.Cpf);

			_userService
				.Setup(x => x.GetByCpfAsync(It.IsAny<string>()))
				.ReturnsAsync(new UserDTO(user));

			// act
			var response = await _loginController.LoginByCpf(request) as ObjectResult;

			// assert
			response.Should().NotBeNull();
			response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
			response?.Value.Should().BeOfType<ReturnAPI<UserDTO>>();
		}
	}
}