using BurguerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurguerRoyale.Infrastructure.Context.EntityConfigs
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
				.HasOne(x => x.Status)
				.WithMany(x => x.Orders)
				.HasForeignKey(x => x.StatusId);

			builder
				.HasMany(x => x.OrderProducts)
				.WithOne(x => x.Order)
				.HasForeignKey(x => x.OrderId);
		}
	}
}
