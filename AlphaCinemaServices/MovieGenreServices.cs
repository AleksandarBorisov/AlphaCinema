using AlphaCinemaData.Models;
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
        private readonly IRepository<Movie> movieRepo;
        private readonly IRepository<Genre> genreRepo;
        
        public MovieGenreServices(IRepository<MovieGenre> movieRepository,
            IRepository<Movie> movieRepo, IRepository<Genre> genreRepo)
        {
            this.repository = movieRepository;
            this.movieRepo = movieRepo;
            this.genreRepo = genreRepo;
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

            var movie = this.movieRepo.All()
                .Where(m => m.Name == movieName)
                .Select(m => m.MovieGenres)
                .FirstOrDefault();


            var genres = movie
                .Select(movieGenre => movieGenre.GenreId.ToString()).ToList();

            //var genres = this.movieRepo.All()
            //    .Where(movie => movie.Name == movieName)
            //    .Select(movie => movie.MovieGenres
            //    .Select(movieGenre => movieGenre.GenreId.ToString())).ToList();

            //var movie = this.movieRepo.All()
            //    .Where(mov => mov.Name == movieName)
            //    .FirstOrDefault();



            return genres;
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
