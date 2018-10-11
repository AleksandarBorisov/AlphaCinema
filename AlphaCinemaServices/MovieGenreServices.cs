using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class MovieGenreServices : IMovieGenreServices
    {
		private readonly IUnitOfWork unitOfWork;
        
        public MovieGenreServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
        }

        public List<string> GetGenreIDsByMovie(string movieName)
        {
            var genreIDs = this.unitOfWork.Movies.All()
                .Where(movie => movie.Name == movieName)
                .Select(movie => movie.MovieGenres
                    .Select(movieGenre => movieGenre.GenreId.ToString())
                    .ToList())
                .FirstOrDefault();
            
            return genreIDs;
        }

        public List<string> GetGenreNamesByMovie(string movieName)
        {
            var genresNames = this.unitOfWork.Movies.All()
                .Where(movie => movie.Name == movieName)
                .Select(movie => movie.MovieGenres
                    .Select(movieGenre => movieGenre.Genre.Name)
                    .ToList())
                .FirstOrDefault();

            return genresNames;
        }

        public List<string> GetMovieIDsByGenreName(string genreName)
        {
			var movieIDs = this.unitOfWork.Genres.All()
				.Where(g => g.Name == genreName)
				.Select(mg => mg.MoviesGenres
                    .Select(m => m.MovieId.ToString())
                    .ToList())
				.FirstOrDefault();
            
            return movieIDs;
        }

        public List<string> GetMovieNamesByGenreName(string genreName)
        {
            var moviesNames = this.unitOfWork.Genres.All()
                .Where(g => g.Name == genreName)
                .Select(mg => mg.MoviesGenres
                    .Select(m => m.Movie.Name)
                    .ToList())
                .FirstOrDefault();

            return moviesNames;
        }

		public List<string> GetMovieNamesByGenreID(string genreID)
		{
			var moviesNames = this.unitOfWork.Genres.All()
				.Where(g => g.Id.ToString() == genreID)
				.Select(mg => mg.MoviesGenres
					.Select(m => m.Movie.Name)
					.ToList())
				.FirstOrDefault();

			return moviesNames;
		}
	}
}
