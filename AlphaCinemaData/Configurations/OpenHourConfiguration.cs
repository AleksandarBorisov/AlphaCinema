using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class OpenHourConfiguration : IEntityTypeConfiguration<OpenHour>
	{
		public void Configure(EntityTypeBuilder<OpenHour> builder)
		{
			builder
				.HasKey(opHour => opHour.Id);

			builder
				.Property(opHour => opHour.StartHour)
				.HasMaxLength(6);

			builder
				.HasIndex(opHour => opHour.StartHour)
				.IsUnique(true);
		}
	}
}
