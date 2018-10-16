using AlphaCinemaData.Configurations;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using System;

namespace AlphaCinemaData.Context
{
    public class AlphaCinemaContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OpenHour> OpenHours { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }
        private DbContextOptions<AlphaCinemaContext> options;

        public AlphaCinemaContext()
        {

        }

        public AlphaCinemaContext(DbContextOptions<AlphaCinemaContext> options) : base(options)
        {
            this.options = options;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			string machineName = Environment.MachineName;
			if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
				.UseSqlServer($"Server ={machineName}; Database = AlphaCinema; Trusted_Connection = True;");
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.ApplyConfiguration(new CityConfiguration());
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new MovieConfiguration());
			modelBuilder.ApplyConfiguration(new MovieGenreConfiguration());
			modelBuilder.ApplyConfiguration(new OpenHourConfiguration());
			modelBuilder.ApplyConfiguration(new ProjectionConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new WatchedMovieConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
