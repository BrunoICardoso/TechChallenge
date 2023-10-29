using BurgerRoyale.API.ConfigController;
using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Get a list of users", Description = "Retrieves a list of users based on the specified type.")]
        [ProducesResponseType(typeof(IEnumerable<ReturnAPI<UserDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUsers([FromQuery] UserType? userType)
		{
			var users = await _userService.GetUsersAsync(userType);

			return IStatusCode(new ReturnAPI<IEnumerable<UserDTO>>(users));
		}

		[HttpGet("{id:Guid}")]
        [SwaggerOperation(Summary = "Get an user by ID", Description = "Retrieves an user by its ID.")]
        [ProducesResponseType(typeof(ReturnAPI<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
		{
			var user = await _userService.GetByIdAsync(id);

			return IStatusCode(new ReturnAPI<UserDTO>(user));
		}

		[HttpPost]
        [SwaggerOperation(Summary = "Add a new user", Description = "Creates a new user.")]
        [ProducesResponseType(typeof(ReturnAPI<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateUser([FromBody] RequestUserDTO user)
		{
			var createdUser = await _userService.CreateAsync(user);

			return IStatusCode(new ReturnAPI<UserDTO>(HttpStatusCode.Created, createdUser));
		}

		[HttpPut("{id:Guid}")]
        [SwaggerOperation(Summary = "Update an user", Description = "Updates an existing user by its ID.")]
        [ProducesResponseType(typeof(ReturnAPI<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateUser(
			[FromRoute] Guid id,
			[FromBody] RequestUserDTO user
		)
		{
			var updatedUser = await _userService.UpdateAsync(id, user);

			return IStatusCode(new ReturnAPI<UserDTO>(updatedUser));
		}

		[HttpDelete("{id:Guid}")]
        [SwaggerOperation(Summary = "Delete an user by ID", Description = "Deletes an user by its ID.")]
        [ProducesResponseType(typeof(ReturnAPI<HttpStatusCode>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ReturnAPI), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
		{
			await _userService.DeleteAsync(id);

			return IStatusCode(new ReturnAPI(HttpStatusCode.NoContent));
		}
	}
}
