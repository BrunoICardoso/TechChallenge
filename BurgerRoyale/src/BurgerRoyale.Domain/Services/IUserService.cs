using BurgerRoyale.Domain.Entities;

namespace BurgerRoyale.Domain.Services
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByCpf(string cpf);
        Task<bool> Update(User user);
        Task<bool> Delete(string cpf);
    }
}