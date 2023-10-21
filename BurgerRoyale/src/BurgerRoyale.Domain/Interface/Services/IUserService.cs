using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IUserService
	{
		Task<UserDTO> GetByIdAsync(Guid userId);

		Task<IEnumerable<UserDTO>> GetUsersAsync(UserType? userType);

		Task<UserDTO> CreateAsync(RequestUserDTO model);

		Task DeleteAsync(Guid userId);

		Task<UserDTO> GetByCpfAsync(string cpf);

		Task<UserDTO> UpdateAsync(Guid userId, RequestUserDTO model);
	}
}