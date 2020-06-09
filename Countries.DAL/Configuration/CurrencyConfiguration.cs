using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.DAL.Configuration
{
	public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
	{
		public void Configure(EntityTypeBuilder<Currency> builder)
		{
			builder.Property(c => c.Code).HasMaxLength(5).IsRequired();
			builder.Property(c => c.Name).HasMaxLength(30).IsRequired();
		}
	}
}
