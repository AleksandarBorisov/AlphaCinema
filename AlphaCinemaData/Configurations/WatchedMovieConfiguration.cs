using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

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
		}
	}
}
