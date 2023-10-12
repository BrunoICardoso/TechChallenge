using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	public class ProductCategoryEntityConfig : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();

			builder
				.HasMany(x => x.Products)
				.WithOne(x => x.Category)
				.HasForeignKey(x => x.CategoryId);

			builder
				.HasData(
					new ProductCategory("Lanche"),
					new ProductCategory("Acompanhamento"),
					new ProductCategory("Bebida"),
					new ProductCategory("Sobremesa")
				);
		}
	}
}
