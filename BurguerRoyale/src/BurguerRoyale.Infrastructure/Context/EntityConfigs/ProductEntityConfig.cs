using BurguerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurguerRoyale.Infrastructure.Context.EntityConfigs
{
	public class ProductEntityConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder.Property(x => x.Id)
				.HasColumnName("Id");

			builder.Property(x => x.Name)
				.HasColumnName("Name");

			builder.Property(x => x.Category)
				.HasColumnName("Category");

			builder.Property(x => x.Price)
				.HasColumnName("Price");

            builder.Property(x => x.Price)
				.HasColumnName("Teste");

        }
    }
}
