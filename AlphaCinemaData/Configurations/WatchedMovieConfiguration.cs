using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
    public class WatchedMovieConfiguration : IEntityTypeConfiguration<WatchedMovie>
    {
        public void Configure(EntityTypeBuilder<WatchedMovie> builder)
        {
            builder
                .HasKey(wm => new
                {
                    wm.UserId,
                    wm.ProjectionId
                });

			builder
				.HasOne(u => u.User)
				.WithMany(wm => wm.WatchedMovies)
				.HasForeignKey(wm => wm.UserId);

			builder
				.HasOne(p => p.Projection)
				.WithMany(pr => pr.WatchedMovies)
				.HasForeignKey(p => p.ProjectionId);
        }
    }
}
