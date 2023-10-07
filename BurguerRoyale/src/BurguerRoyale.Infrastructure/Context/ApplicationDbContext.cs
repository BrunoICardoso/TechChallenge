using BurguerRoyale.Domain.Entities;
using BurguerRoyale.Infrastructure.Context.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace BurguerRoyale.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ProductEntityConfig());
		}
	}
}
