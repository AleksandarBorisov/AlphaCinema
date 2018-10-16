using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class CityConfiguration : IEntityTypeConfiguration<City>
	{
		public void Configure(EntityTypeBuilder<City> builder)
		{
			builder
				.ToTable("Cities");

			builder
				.HasKey(c => c.Id);

			builder
				.HasIndex(c => c.Name)
				.IsUnique(true);

			builder
				.Property(c => c.Name)
				.IsRequired(true)
				.HasMaxLength(50);

			builder
				.HasMany(p => p.Projections)
				.WithOne(c => c.City)
				.HasForeignKey(p => p.CityId);
		}
	}
}
