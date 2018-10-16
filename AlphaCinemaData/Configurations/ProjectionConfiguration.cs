using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class ProjectionConfiguration : IEntityTypeConfiguration<Projection>
	{
		public void Configure(EntityTypeBuilder<Projection> builder)
		{
			builder
				.HasKey(p => p.Id);

			builder
				.HasIndex(p => new
				{
					p.MovieId,
					p.CityId,
					p.OpenHourId
				})
				.IsUnique(true);


			builder
				.HasOne(m => m.Movie)
				.WithMany(p => p.Projections)
				.HasForeignKey(p => p.MovieId);

			builder
				.HasOne(c => c.City)
				.WithMany(p => p.Projections)
				.HasForeignKey(p => p.CityId);

			builder
				.HasOne(oh => oh.OpenHour)
				.WithMany(p => p.Projections)
				.HasForeignKey(p => p.OpenHourId);
		}
	}
}
