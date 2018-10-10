using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
	{
		public void Configure(EntityTypeBuilder<MovieGenre> builder)
		{
			builder
				.HasKey(mg => new
				{
					mg.MovieId,
					mg.GenreId
				});
		}
	}
}
