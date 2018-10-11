using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
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

		public void AddNew(Movie movie, Genre genre)
		{
			 // трябва да се добави добавяне на нов жанр и филм, малко по-късно ще го оправя
			if (IfExist(movie.Name, genre.Name) && IsDeleted(movie.Name, genre.Name))
			{
				var movieGenreObj = this.unitOfWork.MovieGenres.All()
					.FirstOrDefault(mg => mg.Movie.Name == movie.Name && mg.Genre.Name == genre.Name);
				movieGenreObj.IsDeleted = false;
				this.unitOfWork.SaveChanges();
			}
			else if (IfExist(movie.Name, genre.Name) && !IsDeleted(movie.Name, genre.Name))
			{
				throw new EntityAlreadyExistsException("That link is already added.");
			}
			else
			{
				var movieGenreObj = new MovieGenre()
				{
					Movie = movie,
					MovieId = movie.Id,
					Genre = genre,
					GenreId = genre.Id
				};
				this.unitOfWork.MovieGenres.Add(movieGenreObj);
				this.unitOfWork.SaveChanges();
			}
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

        public List<string> GetMovieIDsByGenre(string genreName)
        {
			var movieIDs = this.unitOfWork.Genres.All()
				.Where(g => g.Name == genreName)
				.Select(mg => mg.MoviesGenres
                    .Select(m => m.MovieId.ToString())
                    .ToList())
				.FirstOrDefault();
            
            return movieIDs;
        }

        public List<string> GetMovieNamesByGenre(string genreName)
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

		private bool IfExist(string movieName, string genreName)
		{
			return this.unitOfWork.MovieGenres.AllAndDeleted()
				.Where(mg => mg.Movie.Name == movieName && mg.Genre.Name == genreName)
				.FirstOrDefault() == null ? false : true;
		}

		private bool IsDeleted(string movieName, string genreName)
		{
			return this.unitOfWork.MovieGenres.AllAndDeleted()
				.Where(mg => mg.Movie.Name == movieName && mg.Genre.Name == genreName)
				.FirstOrDefault()
				.IsDeleted;
		}

	}
}
