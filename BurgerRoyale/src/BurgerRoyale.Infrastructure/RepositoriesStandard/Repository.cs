using BurgerRoyale.Domain.Interface.RepositoriesStandard;
using BurgerRoyale.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BurgerRoyale.Infrastructure.RepositoriesStandard
{
	public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly ApplicationDbContext _context;

		public Repository(ApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async Task<TEntity?> GetByIdAsync(Guid id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().Where(predicate).ToListAsync();
		}

		public async Task<TEntity> FindFirstDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
		}

		public async Task AddAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task AddRangeAsync(IEnumerable<TEntity> entities)
		{
			await _context.Set<TEntity>().AddRangeAsync(entities);
			await _context.SaveChangesAsync();
		}

		public void Remove(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
			_context.SaveChanges();
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			_context.Set<TEntity>().RemoveRange(entities);
			_context.SaveChanges();
		}

		public async Task UpdateAsync(TEntity entity)
		{
			_context.Set<TEntity>().Update(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().AnyAsync(predicate);
		}
	}
}
