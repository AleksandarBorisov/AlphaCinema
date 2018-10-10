using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class MovieGenreServices : IMovieGenreServices
    {
        private readonly IRepository<MovieGenre> repository;
        
        public MovieGenreServices(IRepository<MovieGenre> movieRepository)
        {
            this.repository = movieRepository;
        }

        public List<string> GetGenreIDsByMovie(string movieName)
        {
            //TODO
            //Throws an exception: movie name is null
            var genreIDs = this.repository.All()
                .Where(mg => mg.Movie.Name == movieName)
                .Select(mg => mg.GenreId.ToString())
                .ToList();

            return genreIDs;
        }

        public List<string> GetMovieIDsByGenre(string genreName)
        {
            //TODO
            //Throws an exception: genre name is null
            var movieIDs = this.repository.All()
                .Where(mg => mg.Genre.Name == genreName)
                .Select(mg => mg.MovieId.ToString())
                .ToList();

            return movieIDs;
        }
    }
}
