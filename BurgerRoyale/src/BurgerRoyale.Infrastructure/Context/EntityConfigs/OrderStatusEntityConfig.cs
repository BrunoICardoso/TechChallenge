using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	public class OrderStatusEntityConfig : IEntityTypeConfiguration<OrderStatus>
	{
		public void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();

			builder
				.HasMany(x => x.Orders)
				.WithOne(x => x.Status)
				.HasForeignKey(x => x.StatusId);

			builder
				.HasData(
					new OrderStatus("Recebido"),
					new OrderStatus("Em preparação"),
					new OrderStatus("Pronto"),
					new OrderStatus("Finalizado")
				);
		}
	}
}
