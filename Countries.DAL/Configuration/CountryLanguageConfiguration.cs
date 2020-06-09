using Countries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Countries.DAL.Configuration
{
	public class CountryLanguageConfiguration : IEntityTypeConfiguration<CountryLanguage>
	{
		public void Configure(EntityTypeBuilder<CountryLanguage> builder)
		{
			builder.HasAlternateKey(cl => new { cl.CountryId, cl.LanguageId });
			builder.HasOne(cl => cl.Language)
				.WithMany(l => l.CountryLanguages)
				.HasForeignKey(cl => cl.LanguageId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
			builder.HasOne(cl => cl.Country)
				.WithMany(c => c.CountryLanguages)
				.HasForeignKey(cl => cl.CountryId)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
