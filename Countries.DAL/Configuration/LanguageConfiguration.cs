using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.DAL.Configuration
{
	public class LanguageConfiguration : IEntityTypeConfiguration<Language>
	{
		public void Configure(EntityTypeBuilder<Language> builder)
		{
			builder.Property(l => l.Name).HasMaxLength(40).IsRequired();
			builder.Property(l => l.Iso639_1).HasMaxLength(2);
			builder.Property(l => l.Iso639_2).HasMaxLength(3);

			builder.HasIndex(l => new { l.Iso639_1, l.Iso639_2 })
				.HasName("uq_iso_key")
				.IsUnique();
		}
	}
}
