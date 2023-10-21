using BurgerRoyale.Domain.Interface.RepositoriesStandard;
using BurgerRoyale.Infrastructure.Context;

namespace BurgerRoyale.Infrastructure.RepositoriesStandard
{
	public abstract class DomainRepository<TEntity> : Repository<TEntity>,
													  IDomainRepository<TEntity> where TEntity : class
	{

		private bool _disposed = false;

		protected DomainRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}

				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~DomainRepository()
		{
			Dispose(false);
		}
	}
}
