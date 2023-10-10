﻿using BurguerRoyale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BurguerRoyale.Infrastructure.Context.EntityConfigs
{
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
		}
	}
}