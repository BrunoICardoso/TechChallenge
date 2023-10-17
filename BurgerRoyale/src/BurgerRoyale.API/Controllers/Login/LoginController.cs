using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;

namespace BurgerRoyale.API.Controllers.User
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginController : BaseController
	{
		private readonly IUserService _userService;

		public LoginController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<IActionResult> LoginByCpf([FromBody] LoginDTO loginModel)
		{
			var user = await _userService.GetByCpf(loginModel.Cpf);

			return IStatusCode(new ReturnAPI<UserDTO>(user));
		}
	}
}
