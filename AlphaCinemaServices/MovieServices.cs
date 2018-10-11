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

        public string GetID(string movieName)
		{
            if(IfExist(movieName) == null)
            {
                throw new EntityDoesntExistException($"Movie {movieName} is not present in the database.");
            }

            var id = this.unitOfWork.Movies.All()
				.Where(m => m.Name == movieName)
				.Select(m => m.Id).FirstOrDefault();

			return id.ToString();
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
            if (IfExist(name) != null && IsDeleted(name))
            {
                var genre = this.unitOfWork.Genres.All()
                    .FirstOrDefault(g => g.Name == name);
                genre.IsDeleted = false;
				this.unitOfWork.SaveChanges();
            }
            else if (IfExist(name) != null && !IsDeleted(name))
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

        private Movie IfExist(string movieName)
        {
            return this.unitOfWork.Movies.AllAndDeleted()
                .Where(movie => movie.Name == movieName)
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
