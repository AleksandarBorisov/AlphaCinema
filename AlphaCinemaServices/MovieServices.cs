
using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class MovieServices : IMovieServices
    {
        private readonly IUnitOfWork unitOfWork;

        public MovieServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int GetID(string movieName)
		{
            var oldMovie = IfExist(movieName);

            if (oldMovie == null || oldMovie.IsDeleted)
            {//Ако няма такова или е изтрито
                throw new EntityDoesntExistException($"Movie {movieName} is not present in the database.");
            }

            var id = this.unitOfWork.Movies.All()
				.Where(m => m.Name == movieName)
				.Select(m => m.Id)
                .FirstOrDefault();

			return id;
		}

        public void AddNewMovie(string name, string description, int releaseYear, int duration)
        {
            if (name.Length > 50)
            {
                throw new ArgumentException("Movie Name can't be more than 50 characters");
            }

            if (description.Length > 150)
            {
                throw new ArgumentException("Movie Description can't be more than 150 characters");
            }

            var oldMovie = IfExist(name);

            if (oldMovie != null && oldMovie.IsDeleted)
            {
                oldMovie.IsDeleted = false;
                //TO DO implement cascade renew of navigational movie properties
				this.unitOfWork.SaveChanges();
            }
            else if (oldMovie != null && !oldMovie.IsDeleted)
            {
                throw new EntityAlreadyExistsException("Movie is already present in the database.");
            }
            else
            {
                var movie = new Movie()
                {
                    Name = name,
                    Description = description,
                    ReleaseYear = releaseYear,
                    Duration = duration
                };
                this.unitOfWork.Movies.Add(movie);
                this.unitOfWork.SaveChanges();

            }
        }

        public void DeleteMovie(string movieName)
        {
            var oldMovie = IfExist(movieName);

            if (oldMovie == null)
            {
                throw new EntityDoesntExistException("\nMovie is not present in the database.");
            }
            else if (oldMovie != null && oldMovie.IsDeleted)
            {
                throw new EntityDoesntExistException($"Movie {movieName} is not present in the database.");
            }
            var entity = this.unitOfWork.Movies.All()
                .Where(mov => mov.Name == movieName)
                .FirstOrDefault();
            //TO DO implement cascade delete
            this.unitOfWork.Movies.Delete(entity);
            this.unitOfWork.Movies.Save();
        }
        
        private Movie IfExist(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
			    .Where(m => m.Name == movieName)
			    .FirstOrDefault();
        }

        public List<string> GetMovieNamesByCityIDGenreID(
            string genreIDAsString,
            string cityIDAsString)
        {
            int genreID = int.Parse(genreIDAsString);
            int cityID = int.Parse(cityIDAsString);
            var movies = this.unitOfWork.Movies.All()
            .Where(movie => movie.MovieGenres.Any(mg => mg.GenreId == genreID))
            .Where(movie => movie.Projections.Any(p => p.CityId == cityID))
            .Select(movie => movie.Name).ToList();

            return movies;
        }
    }
}
