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
        public async Task GivenGetUsersRequest_WhenGetUsers_ThenShouldReturnListWithStatusOk()
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

        [Fact]
        public async Task GivenGetUserRequest_WhenGetUser_ThenShouldReturnUserWithStatusOk()
        {
            // arrange
            var user = UserMock.Get();

            _userService
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new UserDTO(user));

            // act
            var response = await _userController.GetUser(user.Id) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response?.Value.Should().BeOfType<ReturnAPI<UserDTO>>();

            var data = ((ReturnAPI<UserDTO>?) response?.Value)?.Data;

            data?.Id.Should().Be(user.Id);
        }

        [Fact]
        public async Task GivenRequestUserDto_WhenCreateUser_ThenShouldReturnCreatedUserWithStatusCreated()
        {
            // arrange
            var user = UserMock.Get();

            var request = new RequestUserDTO()
            {
                Cpf = user.Cpf,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType
            };

            _userService
                .Setup(x => x.CreateAsync(request))
                .ReturnsAsync(new UserDTO(user));

            // act
            var response = await _userController.CreateUser(request) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.Created);
            response?.Value.Should().BeOfType<ReturnAPI<UserDTO>>();

            var data = ((ReturnAPI<UserDTO>?)response?.Value)?.Data;

            data?.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task GivenRequestUserDtoAndId_WhenUpdateUser_ThenShouldReturnUpdatedUserWithStatusOk()
        {
            // arrange
            var user = UserMock.Get();

            var request = new RequestUserDTO()
            {
                Cpf = user.Cpf,
                Email = user.Email,
                Name = user.Name,
                UserType = user.UserType
            };

            _userService
                .Setup(x => x.UpdateAsync(user.Id, request))
                .ReturnsAsync(new UserDTO(user));

            // act
            var response = await _userController.UpdateUser(user.Id, request) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            response?.Value.Should().BeOfType<ReturnAPI<UserDTO>>();

            var data = ((ReturnAPI<UserDTO>?)response?.Value)?.Data;

            data?.Email.Should().Be(user.Email);
        }

        [Fact]
        public async Task GivenUserId_WhenDeleteUser_ThenShouldReturnStatusNoContent()
        {
            // arrange
            var userId = Guid.NewGuid();

            // act
            var response = await _userController.DeleteUser(userId) as ObjectResult;

            // assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
    }
}
