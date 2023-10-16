using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
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

		[HttpGet("{cpf}")]
		public async Task<IActionResult> GetUser([FromRoute] string cpf)
		{
			var user = await _userService.GetByCpf(cpf);

			return IStatusCode(
				new ReturnAPI<UserDTO>(user)
			);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
		{
			var createdUser = await _userService.CreateAsync(user);

			return IStatusCode(new ReturnAPI<UserDTO>(HttpStatusCode.Created, createdUser));
		}

		[HttpPut("{userId}")]
		public async Task<IActionResult> UpdateUser
		(
			[FromRoute] Guid userId,
			[FromBody] UserDTO user
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
