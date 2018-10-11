using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder
				.ToTable("Cities");

			builder
				.HasIndex(c => c.Name)
				.IsUnique(true);

			builder
				.Property(c => c.Name)
				.IsRequired(true)
				.HasMaxLength(50);
		}
	}
}
