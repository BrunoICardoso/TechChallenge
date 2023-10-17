using BurgerRoyale.Domain.DTO.Users;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IUserService
	{
		Task<UserDTO> GetById(Guid userId);

		Task<IEnumerable<UserDTO>> GetUsers(UserType? userType);

		Task<UserDTO> CreateAsync(RequestUserDTO model);

		Task Delete(Guid userId);

		Task<UserDTO> GetByCpf(string cpf);

		Task<UserDTO> Update(Guid userId, RequestUserDTO model);
	}
}