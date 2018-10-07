using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaData.Context
{
    public class AlphaCinemaContext : DbContext, IAlphaCinemaContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MoviesGenres { get; set; }
        public DbSet<Projection> MoviesProjections { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Projection> Projections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //Angel
                    .UseSqlServer("Server =.\\ANGELSQL; Database = AlphaCinema; Trusted_Connection = True;");
                    //Krasi
                    //.UseSqlServer("Server =.\\-------; Database = AlphaCinema; Trusted_Connection = True;");
                    //Sasho
                    //.UseSqlServer("Server =.\\-------; Database = AlphaCinema; Trusted_Connection = True;");

            }

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //MoviesGenres
            modelBuilder
                .Entity<MovieGenre>()
                .HasKey(movieGenres => new
                {
                    movieGenres.MovieId,
                    movieGenres.GenreId
                });

            modelBuilder
               .Entity<WatchedMovie>()
               .HasKey(watchedMovie => new
               {
                   watchedMovie.ProjectionId,
                   watchedMovie.UserId
               });

            //Projections
            //modelBuilder
            //    .Entity<Projection>()
            //    .HasKey(pr => pr.Id);

            base.OnModelCreating(modelBuilder);
        }

        
    }
}
