using BurgerRoyale.API.Controllers.User;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using BurgerRoyale.UnitTests.Domain.EntitiesMocks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Xunit;

namespace BurgerRoyale.UnitTests.API.Controllers.User
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService;

        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userService = new Mock<IUserService>();

            _userController = new UserController(_userService.Object);
        }

        [Fact]
        public async Task GivenGetUsersRequest_WhenGetUsers_ThenShouldReturnList()
        {
            // arrange
            _userService
                .Setup(x => x.GetUsersAsync(It.IsAny<UserType>()))
                .ReturnsAsync(new List<UserDTO>());

            // act
            var response = await _userController.GetUsers(UserType.Customer) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response?.Value.Should().BeOfType<ReturnAPI<IEnumerable<UserDTO>>>();
        }
    }
}
