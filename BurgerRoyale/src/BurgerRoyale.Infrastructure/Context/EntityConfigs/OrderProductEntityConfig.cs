using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	[ExcludeFromCodeCoverage]
	public class OrderProductEntityConfig : IEntityTypeConfiguration<OrderProduct>
	{
		public void Configure(EntityTypeBuilder<OrderProduct> builder)
		{
			builder
				.HasKey(x => new { x.OrderId, x.ProductId });

			builder
				.HasOne(x => x.Order)
				.WithMany(x => x.OrderProducts)
				.HasForeignKey(x => x.OrderId);

			builder
				.HasOne(x => x.Product)
				.WithMany(x => x.OrderProducts)
				.HasForeignKey(x => x.ProductId);
		}
	}
}
