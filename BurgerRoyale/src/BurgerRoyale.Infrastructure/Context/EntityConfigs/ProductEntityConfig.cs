using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	public class ProductEntityConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();

			builder
				.HasMany(x => x.Images)
				.WithOne(x => x.Product)
				.HasForeignKey(x => x.ProductId);

			builder
				.HasMany(x => x.OrderProducts)
				.WithOne(x => x.Product)
				.HasForeignKey(x => x.ProductId);
		}
	}
}
