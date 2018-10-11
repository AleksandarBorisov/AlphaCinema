using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder
				.ToTable("Genres");

			builder
				.HasIndex(g => g.Name)
				.IsUnique(true);

			builder
				.Property(g => g.Name)
				.IsRequired(true)
				.HasMaxLength(50);
		}
	}
}
