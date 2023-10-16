using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	public class OrderEntityConfig : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();

			builder
				.HasMany(x => x.OrderProducts)
				.WithOne(x => x.Order)
				.HasForeignKey(x => x.OrderId);
		}
	}
}
