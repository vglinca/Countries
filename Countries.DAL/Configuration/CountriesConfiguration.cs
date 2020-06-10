using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.DAL.Configuration
{
	public class CountriesConfiguration : IEntityTypeConfiguration<Country>
	{
		public void Configure(EntityTypeBuilder<Country> builder)
		{
			builder.Property(c => c.Id).IsRequired();
			builder.Property(c => c.Name).HasMaxLength(75);
			builder.Property(c => c.Capital).HasMaxLength(50);
			builder.Property(c => c.NumericCode).HasMaxLength(3).IsRequired();
			builder.HasOne(c => c.Currency)
				.WithMany(c => c.Countries)
				.HasForeignKey(c => c.CurrencyId)
				.OnDelete(DeleteBehavior.SetNull)
				.IsRequired();
		}
	}
}
