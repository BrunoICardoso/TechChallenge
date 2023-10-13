using BurgerRoyale.Domain.DTO;

namespace BurgerRoyale.Domain.Interface.Services
{
	public interface IUserService
	{
		Task CreateAsync(UserDTO model);

		Task Delete(string cpf);

		Task<UserDTO> GetByCpf(string cpf);

		Task<UserDTO> GetByEmail(string email);

		Task Update(UserDTO model);
	}
}