using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;

namespace AlphaCinemaData.UnitOfWork
{
	public interface IUnitOfWork
	{
		IRepository<City> Cities { get; }
		IRepository<Genre> Genres { get; }
		IRepository<Movie> Movies { get; }
		IRepository<OpenHour> OpenHours { get; }
		IRepository<User> Users { get; }
		IRepository<MovieGenre> MovieGenres { get; }
		IRepository<Projection> Projections { get; }
		IRepository<WatchedMovie> WatchedMovies { get; }
		int SaveChanges();
	}
}