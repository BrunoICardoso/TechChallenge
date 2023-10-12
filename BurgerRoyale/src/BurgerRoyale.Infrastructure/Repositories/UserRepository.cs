using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Infrastructure.Context;
using BurgerRoyale.Infrastructure.RepositoriesStandard;

namespace BurgerRoyale.Infrastructure.Repositories
{
	public class UserRepository : DomainRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}
	}
}