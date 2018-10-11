using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
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
            //TODO
            //Throws an exception: movie name is null
            //With Include bat kras :)

            //var genreIDs = this.repository.All()
            //    .Where(mg => mg.Movie.Name == movieName)
            //    .Select(mg => mg.GenreId.ToString())
            //    //.Include()
            //    .ToList();

            //var movie = this.movieRepo.All()
            //    .Where(m => m.Name == movieName)
            //    .Select(m => m.MovieGenres)
            //    .FirstOrDefault();


            //var genres = movie
            //    .Select(movieGenre => movieGenre.GenreId.ToString()).ToList();

            var genres = this.unitOfWork.Movies.All()
                .Where(movie => movie.Name == movieName)
                .Select(movie => movie.MovieGenres.Select(movieGenre => movieGenre.GenreId.ToString()).ToList())
                .FirstOrDefault();

            //var movie = this.movieRepo.All()
            //    .Where(mov => mov.Name == movieName)
            //    .FirstOrDefault();



            return genres;
        }

        public List<string> GetMovieIDsByGenre(string genreName)
        {
			//TODO
			//Throws an exception: genre name is null
			var movieIDs = this.unitOfWork.Genres.All()
				.Where(g => g.Name == genreName)
				.Select(mg => mg.MoviesGenres.Select(m => m.MovieId.ToString()).ToList())
				.FirstOrDefault();

            return movieIDs;
        }
    }
}
