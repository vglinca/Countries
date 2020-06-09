using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.DAL.Configuration
{
	public class ContinentConfiguration : IEntityTypeConfiguration<Region>
	{
		public void Configure(EntityTypeBuilder<Region> builder)
		{
			builder.Property(c => c.Id).IsRequired();
			builder.Property(c => c.Name).HasMaxLength(25);
			builder
				.HasMany(c => c.Countries)
				.WithOne(c => c.Region)
				.HasForeignKey(c => c.RegionId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
