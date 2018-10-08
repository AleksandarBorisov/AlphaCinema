using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using Microsoft.EntityFrameworkCore;

namespace AlphaCinemaData.Context
{
    public interface IAlphaCinemaContext
    {
        DbSet<City> Cities { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<MovieGenre> MoviesGenres { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Projection> Projections { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<WatchedMovie> WatchedMovies { get; set; }
        DbSet<OpenHour> OpenHours { get; set; }
        
        int SaveChanges();
        void Clear();
    }
}
