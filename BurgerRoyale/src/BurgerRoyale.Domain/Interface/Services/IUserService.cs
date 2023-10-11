using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.ResponseDefault;

namespace BurgerRoyale.Domain.Interface.Services
{
    public interface IUserService
    {
        Task<ReturnAPI> CreateAsync(UserDTO model);
        Task<ReturnAPI> Delete(string cpf);
        Task<ReturnAPI<UserDTO>> GetByCpf(string cpf);
        Task<ReturnAPI<UserDTO>> GetByEmail(string email);
        Task<ReturnAPI> Update(UserDTO model);
    }

}