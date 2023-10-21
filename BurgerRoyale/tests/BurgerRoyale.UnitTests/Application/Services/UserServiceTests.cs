using BurgerRoyale.Application.Services;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.UnitTests.Domain.EntitiesMocks;
using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace BurgerRoyale.UnitTests.Application.Services
{
	public class UserServiceTests
	{
		private readonly Mock<IUserRepository> _userRepository;

		private readonly UserService _service;

		public UserServiceTests()
		{
			_userRepository = new Mock<IUserRepository>();

			_service = new UserService(_userRepository.Object);
		}

		[Fact]
		public async Task GivenGetByCpfRequest_WhenUserDoesNotExists_ThenShouldThrowNotFoundException()
		{
			// arrange
			var cpf = "123.456.789-10";

			// act
			Func<Task> task = async () => await _service.GetByCpfAsync(cpf);

			// assert
			await task.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("CPF não encontrado");
		}

		[Theory]
		[InlineData(UserType.Customer, "Cliente")]
		[InlineData(UserType.Employee, "Funcionário")]
		public async Task GivenGetByCpfRequest_WhenUserExists_ThenShouldReturnUserDto
		(
			UserType userType,
			string userTypeDescription
		)
		{
			// arrange
			var cpf = "123.456.789-10";

			var mockedUser = UserMock.Get(cpf, userType: userType);

			_userRepository
				.Setup(r => r.FindFirstDefaultAsync(
					x => x.Cpf == "12345678910"
				))
				.ReturnsAsync(mockedUser);

			// act
			var response = await _service.GetByCpfAsync(cpf);

			// assert
			response.Should().BeOfType<UserDTO>();
			response.Cpf.Should().Be(cpf);
			response.UserTypeDescription.Should().Be(userTypeDescription);
		}

		[Fact]
		public async Task GivenCreateRequest_WhenUserAlreadyExists_ThenShouldThrowDomainException()
		{
			// arrange
			var request = new RequestUserDTO
			{
				Cpf = "123.456.789-10"
			};

			_userRepository
				.Setup(r => r.AnyAsync(x => x.Cpf == "12345678910"))
				.ReturnsAsync(true);

			// act
			Func<Task> task = async () => await _service.CreateAsync(request);

			// assert
			await task.Should()
				.ThrowAsync<DomainException>()
				.WithMessage("CPF já cadastrado");
		}

		[Fact]
		public async Task GivenCreateRequest_WhenUserDoesNotExist_ThenShouldCreateUser()
		{
			// arrange
			var request = new RequestUserDTO
			{
				Cpf = "123.456.789-10",
				Name = "Test Name",
				Email = "test@email.com",
				UserType = UserType.Customer
			};

			// act
			var response = await _service.CreateAsync(request);

			// assert
			_userRepository
				.Verify(
					x => x.AddAsync(It.IsAny<User>()),
					Times.Once
				);

			response.Should().BeOfType<UserDTO>();
			response.Cpf.Should().Be(request.Cpf);
		}

		[Fact]
		public async Task GivenUpdateRequest_WhenUserDoesNotExist_ThenShouldThrowNotFoundException()
		{
			// arrange
			var userId = Guid.NewGuid();
			var request = new RequestUserDTO
			{
				Cpf = "123.456.789-10"
			};

			// act
			Func<Task> task = async () => await _service.UpdateAsync(userId, request);

			// assert
			await task.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("Usuário não encontrado");
		}

		[Fact]
		public async Task GivenUpdateRequest_WhenUserExists_ThenShouldUpdateUser()
		{
			// arrange
			var request = new RequestUserDTO
			{
				Cpf = "123.456.789-10",
				Name = "Updated Name",
				Email = "updated@email.com",
				UserType = UserType.Employee
			};

			var mockedUser = UserMock.Get(
				request.Cpf,
				"Initial Name",
				"old@email.com",
				UserType.Customer
			);

			_userRepository
				.Setup(r => r.FindFirstDefaultAsync(
					It.IsAny<Expression<Func<User, bool>>>()
				))
				.ReturnsAsync(mockedUser);

			// act
			var response = await _service.UpdateAsync(mockedUser.Id, request);

			// assert
			_userRepository
				.Verify(
					x => x.UpdateAsync(It.IsAny<User>()),
					Times.Once
				);

			response.Should().BeOfType<UserDTO>();

			response.Cpf.Should().Be(request.Cpf);
			response.Name.Should().Be(request.Name);
			response.Email.Should().Be(request.Email);
			response.UserType.Should().Be(request.UserType);
		}

		[Fact]
		public async Task GivenDeleteRequest_WhenUserDoesNotExist_ThenShouldThrowNotFoundException()
		{
			// arrange
			var userId = Guid.NewGuid();

			// act
			Func<Task> task = async () => await _service.DeleteAsync(userId);

			// assert
			await task.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("Usuário não encontrado");
		}

		[Fact]
		public async Task GivenDeleteRequest_WhenUserExists_ThenShouldRemoveUser()
		{
			// arrange
			var mockedUser = UserMock.Get();

			_userRepository
				.Setup(r => r.FindFirstDefaultAsync(
					It.IsAny<Expression<Func<User, bool>>>()
				))
				.ReturnsAsync(mockedUser);

			// act
			await _service.DeleteAsync(mockedUser.Id);

			// assert
			_userRepository
				.Verify(
					x => x.Remove(It.IsAny<User>()),
					Times.Once
				);
		}

		[Fact]
		public async Task GivenGetByIdRequest_WhenUserDoesNotExist_ThenShouldThrowNotFoundException()
		{
			// arrange
			var userId = Guid.NewGuid();

			// act
			Func<Task> task = async () => await _service.GetByIdAsync(userId);

			// assert
			await task.Should()
				.ThrowAsync<NotFoundException>()
				.WithMessage("Usuário não encontrado");
		}

		[Fact]
		public async Task GivenGetByIdRequest_WhenUserExists_ThenShouldReturnUserDto()
		{
			// arrange
			var mockedUser = UserMock.Get();

			_userRepository
				.Setup(r => r.FindFirstDefaultAsync(
					It.IsAny<Expression<Func<User, bool>>>()
				))
				.ReturnsAsync(mockedUser);

			// act
			var response = await _service.GetByIdAsync(mockedUser.Id);

			// assert
			response.Should().BeOfType<UserDTO>();

			response.Cpf.Should().Be(Format.FormatCpf(mockedUser.Cpf));
			response.Name.Should().Be(mockedUser.Name);
			response.Email.Should().Be(mockedUser.Email);
			response.UserType.Should().Be(mockedUser.UserType);
		}

		[Fact]
		public async Task GivenGetUsersRequest_WhenNoUserTypeIsInformed_ThenShouldGetAllUsersAndReturnList()
		{
			// arrange
			var mockedUsers = UserMock.GetList(3);

			_userRepository
				.Setup(r => r.GetAllAsync())
				.ReturnsAsync(mockedUsers);

			// act
			var response = await _service.GetUsersAsync(null);

			// assert
			response.Should().BeAssignableTo<IEnumerable<UserDTO>>();
			response.Should().HaveCount(3);

			_userRepository.Verify(
				x => x.GetAllAsync(),
				Times.Once
			);

			_userRepository.Verify(
				x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()),
				Times.Never
			);
		}

		[Theory]
		[InlineData(UserType.Customer)]
		[InlineData(UserType.Employee)]
		public async Task GivenGetUsersRequest_WhenUserTypeIsInformed_ThenShouldGetUsersByUserTypeAndReturnList
		(
			UserType userType
		)
		{
			// arrange
			var mockedUsers = UserMock.GetList(3, userType);

			_userRepository
				.Setup(r => r.FindAsync(It.IsAny<Expression<Func<User, bool>>>()))
				.ReturnsAsync(mockedUsers);

			// act
			var response = await _service.GetUsersAsync(userType);

			// assert
			response.Should().BeAssignableTo<IEnumerable<UserDTO>>();
			response.Should().HaveCount(3);
			response.Should().OnlyContain(x => x.UserType == userType);

			_userRepository.Verify(
				x => x.FindAsync(It.IsAny<Expression<Func<User, bool>>>()),
				Times.Once
			);

			_userRepository.Verify(
				x => x.GetAllAsync(),
				Times.Never
			);
		}
	}
}
