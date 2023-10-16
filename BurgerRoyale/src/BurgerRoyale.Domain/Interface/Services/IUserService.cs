using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IUserService
	{
		Task<UserDTO> CreateAsync(UserDTO model);

		Task Delete(Guid userId);

		Task<UserDTO> GetByCpf(string cpf);

		Task<UserDTO> Update(Guid userId, UserDTO model);
	}
}