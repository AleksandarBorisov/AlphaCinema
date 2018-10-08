using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlphaCinemaData.Context
{
    public class AlphaCinemaContext : DbContext, IAlphaCinemaContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<User> Users { get; set; }
		public DbSet<OpenHour> OpenHours { get; set; }
		public DbSet<WatchedMovie> WatchedMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //Angel
                    .UseSqlServer("Server =.\\ANGELSQL; Database = AlphaCinema; Trusted_Connection = True;");
                    //Krasi
                    //.UseSqlServer("Server =DESKTOP-ETOV; Database = AlphaCinema; Trusted_Connection = True;");
                    //Sasho
                    //.UseSqlServer("Server =.\\-------; Database = AlphaCinema; Trusted_Connection = True;");

            }

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			// Cities
			modelBuilder
				.Entity<City>(city =>
				{
					city.HasKey(g => g.Id);

					city.Property(c => c.Id)
					.ValueGeneratedOnAdd();

					city.Property(c => c.Name)
					.IsRequired()
					.HasMaxLength(50);
				});

			// Genres
			modelBuilder
				.Entity<Genre>(genre =>
				{
					genre.HasKey(g => g.Id);

					genre.Property(g => g.Id)
					.ValueGeneratedOnAdd();

					genre.Property(g => g.Name)
					.IsRequired()
					.HasMaxLength(50);

				});

			// MoviesGenres
			modelBuilder
                .Entity<MovieGenre>()
				.HasKey(movieGenres => new
				{
					movieGenres.MovieId,
					movieGenres.GenreId
				});

			// WatchedMovies
            modelBuilder
               .Entity<WatchedMovie>()
               .HasKey(watchedMovie => new
               {
                   watchedMovie.ProjectionId,
                   watchedMovie.UserId
               });

			// Projections
			modelBuilder
				.Entity<Projection>(projection =>
				{
					projection.HasKey(proj => proj.Id);

					projection.Property(proj => proj.Id)
					.ValueGeneratedOnAdd();

					projection
					.HasIndex(p => new
					{
						p.MovieId, p.CityId, p.OpenHourId
					})
					.IsUnique(true);
				});


            // Users
            modelBuilder
                .Entity<User>(user =>
                {
                    user.HasKey(us => us.Id);

                    user.Property(us => us.Name)
                    .HasMaxLength(50);

                    user.Property(us => us.Age);

                });

            // OpenHours
            modelBuilder
                .Entity<OpenHour>(openHour =>
                {
                    openHour.HasKey(opHour => opHour.Id);

                    openHour.Property(opHour => opHour.StartHour);

                });

            // Movies
            modelBuilder
                .Entity<Movie>(movie =>
                {
                    movie.HasKey(mov => mov.Id);

                    movie.Property(mov => mov.Name)
                    .HasMaxLength(50);

                    movie.Property(mov => mov.Description)
                    .HasMaxLength(60);

                    movie.Property(mov => mov.ReleaseYear);

                    movie.Property(mov => mov.Duration);
                });

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
