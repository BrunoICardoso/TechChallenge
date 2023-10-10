using System.Linq.Expressions;

namespace BurguerRoyale.Domain.RepositoriesStandard
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<TEntity> GetAsync(int id);
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
		Task AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);
		void Remove(TEntity entity);
		void RemoveRange(IEnumerable<TEntity> entities);
		Task UpdateAsync(TEntity entity);
	}
}
