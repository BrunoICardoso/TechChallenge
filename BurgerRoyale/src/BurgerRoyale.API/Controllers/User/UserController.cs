using BurgerRoyale.Domain.Config.EndPoint;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.Validation;
using Microsoft.AspNetCore.Mvc;

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
            return IStatusCode(await _userService.GetByCpf(cpf));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO user)
        {
            return IStatusCode(await _userService.CreateAsync(user));
            
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO user)
        {
            return IStatusCode(await _userService.Update(user));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string cpf)
        {
            return IStatusCode(await _userService.Delete(cpf));
        }
    }
}
