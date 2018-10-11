using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
	public class MovieServices : IMovieServices
	{
		private readonly IRepository<Movie> repository;

		public MovieServices(IRepository<Movie> repository)
		{
			this.repository = repository;
		}

		public string GetID(string movieName)
		{
			var id = repository.All()
				.Where(m => m.Name == movieName)
				.Select(m => m.Id).FirstOrDefault();

			return id.ToString();
		}

		public List<string> GetMovieNames()
		{
			var movieNames = repository.All()
				.Select(movie => movie.Name)
				.ToList();

			return movieNames;
		}

        public void AddNewMovie(string[] movieDetails)
        {
            string movieName = movieDetails[0];
            if (movieName.Length > 50)
            {
                throw new ArgumentException("Movie Name can't be more than 50 characters");
            }
            string movieDescription = movieDetails[1];
            if (movieDescription.Length > 150)
            {
                throw new ArgumentException("Movie Description can't be more than 150 characters");
            }
            if (!int.TryParse(movieDetails[2], out int movieRealeaseYear))
            {
                throw new ArgumentException("Movie ReleaseYear must be integer number");
            }
            if (!int.TryParse(movieDetails[3], out int movieDuration))
            {
                throw new ArgumentException("Movie Duration must be integer number");
            }

            if (IfExist(movieName) && IsDeleted(movieName))
            {
                var genre = repository.All()
                    .FirstOrDefault(g => g.Name == movieName);
                genre.IsDeleted = false;
                repository.Save();
            }
            else if (IfExist(movieName) && !IsDeleted(movieName))
            {
                throw new EntityAlreadyExistsException("Movie is already present in the database.");
            }
            else
            {
                var movie = new Movie()
                {
                    Name = movieName,
                    Description = movieDescription,
                    ReleaseYear = movieRealeaseYear,
                    Duration = movieDuration
                };
                repository.Add(movie);
                repository.Save();
            }
        }

        private bool IfExist(string movieName)
        {
            return repository.All()
                .Where(movie => movie.Name == movieName)
                .FirstOrDefault() == null ? false : true;
        }

        private bool IsDeleted(string movieName)
        {
            return repository.All()
                .Where(g => g.Name == movieName)
                .FirstOrDefault()
                .IsDeleted;
        }
    }
}
