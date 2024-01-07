using FakePaymentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FakePaymentService.Infrastructure.Context.EntityConfigs
{
	[ExcludeFromCodeCoverage]
	public class PaymentEntityConfig : IEntityTypeConfiguration<Payment>
	{
		public void Configure(EntityTypeBuilder<Payment> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();
		}
	}
}
