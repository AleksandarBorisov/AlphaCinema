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
				.HasKey(proj => proj.Id);

			builder
				.Property(proj => proj.Id)
				.ValueGeneratedOnAdd();

			builder
				.HasIndex(p => new
				{
					p.MovieId,
					p.CityId,
					p.OpenHourId
				})
				.IsUnique(true);
		}
	}
}
