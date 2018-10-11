using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{

			builder
				.Property(m => m.Name)
				.HasMaxLength(50);

			builder
				.HasIndex(m => m.Name)
				.IsUnique(true);

			builder
				.Property(m => m.Description)
				.HasMaxLength(150);

			builder
				.Property(m => m.ReleaseYear);

			builder
				.Property(m => m.Duration);
		}
	}
}
