using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Services;
using BurgerRoyale.Domain.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BurgerRoyale.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly string cpfNotFound = "CPF não encontrado.";
        private readonly string cpfAlreadyExists = "CPF já cadastrado.";
        private readonly string invalidCpf = "CPF inválido.";
        private readonly string invalidEmail = "E-mail inválido.";
        private readonly string deleteError = "Erro ao excluir usuário.";
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string cpf)
        {
            var existingUser = await _userService.GetByCpf(cpf);
            if (existingUser != null)
                return Ok(existingUser);
            return BadRequest(cpfNotFound);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Domain.Entities.User user)
        {
            var existingUser = await _userService.GetByCpf(user.Cpf);
            if (existingUser != null)
                return BadRequest(cpfAlreadyExists);

            if (!Validate.IsCpfValid(user.Cpf))
                return BadRequest(invalidCpf);

            if (!Validate.IsEmailValid(user.Email))
                return BadRequest(invalidEmail);

            var createdUser = await _userService.CreateAsync(user);
            return Ok(createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] Domain.Entities.User user)
        {
            if (!Validate.IsEmailValid(user.Email))
                return BadRequest(invalidEmail);

            var userWasUpdated = await _userService.Update(user);
            if (userWasUpdated)
                return Ok(user);

            return BadRequest("Erro ao atualizar usuário.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string cpf)
        {
            var successfulDelete = await _userService.Delete(cpf);
            if(successfulDelete)
                return Ok($"Usuário com CPF {cpf} excluído.");
            return BadRequest(deleteError);
        }
    }
}
