using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.RepositoriesStandard;

namespace BurgerRoyale.Domain.Repositories
{
    public interface IUserRepository : IDomainRepository<User>
    {
        Task<User> GetByCpf(string cpf);
        Task<User> GetByEmail(string email);
    }
}
