using BurgerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace BurgerRoyale.Infrastructure.Context.EntityConfigs
{
	[ExcludeFromCodeCoverage]
	public class UserEntityConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.Id)
				.ValueGeneratedOnAdd();

			builder.Property(x => x.Cpf)
				.HasMaxLength(15);

			builder.Property(x => x.Email)
				.HasMaxLength(100);
		}
	}
}