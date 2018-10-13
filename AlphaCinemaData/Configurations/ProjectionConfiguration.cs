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
				.HasIndex(p => new
				{
					p.MovieId,
					p.CityId,
					p.OpenHourId,
					p.Date
				})
				.IsUnique(true);
		}
	}
}
