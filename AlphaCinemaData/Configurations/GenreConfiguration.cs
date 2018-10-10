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
				.HasKey(g => g.Id);

			builder
				.Property(g => g.Id)
				.ValueGeneratedOnAdd();

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
