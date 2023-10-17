using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO.Users;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BurgerRoyale.API.Controllers.User
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : BaseController
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers([FromQuery] UserType? userType)
		{
			var users = await _userService.GetUsers(userType);

			return IStatusCode(new ReturnAPI<IEnumerable<UserDTO>>(users));
		}

		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUser([FromRoute] Guid userId)
		{
			var user = await _userService.GetById(userId);

			return IStatusCode(new ReturnAPI<UserDTO>(user));
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] RequestUserDTO user)
		{
			var createdUser = await _userService.CreateAsync(user);

			return IStatusCode(new ReturnAPI<UserDTO>(HttpStatusCode.Created, createdUser));
		}

		[HttpPut("{userId}")]
		public async Task<IActionResult> UpdateUser
		(
			[FromRoute] Guid userId,
			[FromBody] RequestUserDTO user
		)
		{
			var updatedUser = await _userService.Update(userId, user);

			return IStatusCode(new ReturnAPI<UserDTO>(updatedUser));
		}

		[HttpDelete("{userId}")]
		public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
		{
			await _userService.Delete(userId);

			return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
		}
	}
}
