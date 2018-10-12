
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
            if(!IfExist(movieName) || IsDeleted(movieName))
            {
                throw new EntityDoesntExistException($"{movieName} is not present in the database.");
            }

            var id = this.unitOfWork.Movies.All()
				.Where(m => m.Name == movieName)
				.Select(m => m.Id)
                .FirstOrDefault();

			return id;
		}

		public List<string> GetMovieNames()
		{
			var movieNames = this.unitOfWork.Movies.All()
				.Select(movie => movie.Name)
				.ToList();

			return movieNames;
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
  
            //Check if exist and if is deleted
            if (IfExist(name) && IsDeleted(name))
            {
                var movie = this.unitOfWork.Movies.AllAndDeleted()
                    .FirstOrDefault(m => m.Name == name);
                movie.IsDeleted = false;

				this.unitOfWork.SaveChanges();
            }
            else if (IfExist(name) && !IsDeleted(name))
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
            if (!IfExist(movieName))
            {
                throw new EntityDoesntExistException("\nMovie is not present in the database.");
            }
            else if (IfExist(movieName) && IsDeleted(movieName))
            {
                throw new EntityDoesntExistException($"Movie {movieName} is not present in the database.");
            }
            var entity = this.unitOfWork.Movies.All()
                .Where(mov => mov.Name == movieName)
                .FirstOrDefault();

            this.unitOfWork.Movies.Delete(entity);
            this.unitOfWork.Movies.Save();
        }
        
        public Movie GetMovieByName(string movieName)
		{
			var movie = unitOfWork.Movies.All()
				.Where(m => m.Name == movieName)
				.FirstOrDefault();
			return movie;
		}

		public List<string> GetMovieNamesByCityIDGenreID(int genreID, int cityID)
		{
			var movies = this.unitOfWork.Movies.All()
			.Where(movie => movie.MovieGenres.Any(mg => mg.GenreId == genreID))
			.Where(movie => movie.Projections.Any(p => p.CityId == cityID))
			.Select(movie => movie.Name).ToList();

			return movies;
		}


		private bool IfExist(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
			    .Where(m => m.Name == movieName)
			    .FirstOrDefault() == null ? false : true;
        }

        private Movie ObjectExist(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
            .Where(m => m.Name == movieName)
            .FirstOrDefault();
        }

        private bool IsDeleted(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
                .Where(g => g.Name == movieName)
                .FirstOrDefault()
                .IsDeleted;
        }
    }
}
