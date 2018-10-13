using AlphaCinemaData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AlphaCinemaData.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder
				.Property(u => u.Name)
				.HasMaxLength(50);

			builder
				.HasIndex(u => u.Name)
				.IsUnique(true);

			builder
				.Property(u => u.Age);
		}
	}
}
