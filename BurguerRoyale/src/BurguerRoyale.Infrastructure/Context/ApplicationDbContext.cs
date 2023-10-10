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
		public DbSet<User> Users { get; set; }
		public DbSet<User> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ProductEntityConfig());
			modelBuilder.ApplyConfiguration(new UserEntityConfig());
			modelBuilder.ApplyConfiguration(new OrderEntityConfig());
			modelBuilder.ApplyConfiguration(new OrderProductEntityConfig());
		}
	}
}
