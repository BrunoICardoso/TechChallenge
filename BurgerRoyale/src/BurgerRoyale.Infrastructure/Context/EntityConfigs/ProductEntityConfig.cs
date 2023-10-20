using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	[ExcludeFromCodeCoverage]
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

			builder
				.HasData(
					new Product("Burger Tradicional", "Hambúrguer de carne bovina.", (decimal)19.20, ProductCategory.Lanche),
					new Product("Burger Duplo Bacon", "Hambúrguer de carne bovina com o dobro de bacon.", (decimal)22.90, ProductCategory.Lanche),
					new Product("Burger Duplo Cheddar", "Hambúrguer de carne bovina com o dobro de cheddar.", (decimal)23.90, ProductCategory.Lanche),
					new Product("Fritas Pequena", "Porção de fritas pequena.", (decimal)4.90, ProductCategory.Acompanhamento),
					new Product("Fritas", "Porção de fritas.", (decimal)6.90, ProductCategory.Acompanhamento),
					new Product("Fritas Grande", "Porção de fritas grande.", (decimal)8.90, ProductCategory.Acompanhamento),
					new Product("Água", "500 ml com ou sem gás", (decimal)4.00, ProductCategory.Bebida),
					new Product("Refrigerante", "Copo 400 ml", (decimal)6.00, ProductCategory.Bebida),
					new Product("Sundae", "Sundae de diversos sabores", (decimal)7.00, ProductCategory.Sobremesa),
					new Product("Sorvete", "Sorvete de diversos sabores", (decimal)7.00, ProductCategory.Sobremesa)
				);
		}
	}
}
