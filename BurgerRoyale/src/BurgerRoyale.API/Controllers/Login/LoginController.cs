using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BurgerRoyale.API.Controllers.Login
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
        [SwaggerOperation(Summary = "Login with CPF", Description = "Login in the system given the CPF.")]
        [ProducesResponseType(typeof(ReturnAPI<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI<UserDTO>), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> LoginByCpf([FromBody] LoginDTO loginModel)
		{
			var user = await _userService.GetByCpfAsync(loginModel.Cpf);

			return IStatusCode(new ReturnAPI<UserDTO>(user));
		}
	}
}