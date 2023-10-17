using System.Linq.Expressions;

namespace BurgerRoyale.Domain.Interface.RepositoriesStandard
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

		Task<TEntity?> GetByIdAsync(Guid id);

		Task<IEnumerable<TEntity>> GetAllAsync();

		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

		Task<TEntity> FindFirstDefaultAsync(Expression<Func<TEntity, bool>> predicate);

		Task AddAsync(TEntity entity);

		Task AddRangeAsync(IEnumerable<TEntity> entities);

		void Remove(TEntity entity);

		void RemoveRange(IEnumerable<TEntity> entities);

		Task UpdateAsync(TEntity entity);
	}
}
