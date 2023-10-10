﻿namespace BurgerRoyale.Domain.RepositoriesStandard
{
	public interface IDomainRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
	{
	}
}