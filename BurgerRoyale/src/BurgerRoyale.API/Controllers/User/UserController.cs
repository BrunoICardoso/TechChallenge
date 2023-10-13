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

		[HttpGet]
		public async Task<IActionResult> GetUser(string cpf)
		{
			var user = await _userService.GetByCpf(cpf);

			return IStatusCode(
				new ReturnAPI<UserDTO>(user)
			);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
		{
			await _userService.CreateAsync(user);

			return IStatusCode(new ReturnAPI(HttpStatusCode.Created));
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
		{
			await _userService.Update(user);

			return IStatusCode(new ReturnAPI());
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteUser(string cpf)
		{
			await _userService.Delete(cpf);

			return IStatusCode(new ReturnAPI());
		}
	}
}
