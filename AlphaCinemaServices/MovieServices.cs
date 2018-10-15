using AlphaCinemaData.Models;
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
		private Movie movie;

		public MovieServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public int GetID(string movieName)
		{
			movie = IfExist(movieName);

			if (movie == null || movie.IsDeleted)
			{//Ако няма такова или е изтрито
				throw new EntityDoesntExistException($"Movie {movieName} is not present in the database.");
			}
			return movie.Id;
		}

		public ICollection<string> GetMovieNames()
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

			movie = IfExist(name);

			if (movie != null)
			{
				if (movie.IsDeleted)
				{
					movie.IsDeleted = false;
					this.unitOfWork.SaveChanges();
					return;
				}
				else
				{
					throw new EntityAlreadyExistsException("Movie is already present in the database.");
				}
			}
			else
			{
				movie = new Movie()
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

		public void UpdateMovie(string name, string description, int releaseYear, int duration)
		{
			if (name.Length > 50)
			{
				throw new ArgumentException("Movie Name can't be more than 50 characters");
			}

			if (description.Length > 150)
			{
				throw new ArgumentException("Movie Description can't be more than 150 characters");
			}

			movie = IfExist(name);

			if (movie != null)
			{//Ако го е имало, се подсигуряваме че не е изтрито
				movie.IsDeleted = false;
			}
			else
			{//В противен случай го няма и хвърляме изключение
				throw new EntityDoesntExistException("Movie is not present in the database, consider using AddMovie.");
			}
			//Ако не сме хвърлили изключение предефинираме филма и го запазваме
			movie.Name = name;
			movie.Description = description;
			movie.ReleaseYear = releaseYear;
			movie.Duration = duration;

			this.unitOfWork.Movies.Update(movie);
			this.unitOfWork.SaveChanges();
		}

		public void DeleteMovie(string movieName)
		{
			movie = IfExist(movieName);

			if (movie == null || movie.IsDeleted)
			{
				throw new EntityDoesntExistException("\nMovie is not present in the database.");
			}

			this.unitOfWork.Movies.Delete(movie);
			this.unitOfWork.SaveChanges();
		}

		private Movie IfExist(string movieName)
		{
			return this.unitOfWork.Movies.AllAndDeleted()
				.Where(m => m.Name == movieName)
				.FirstOrDefault();
		}

		public ICollection<string> GetMovieNamesByCityIDGenreID(int genreID, int cityID)
		{
			var movies = this.unitOfWork.Movies.All()
			.Where(movie => movie.MovieGenres.Any(mg => mg.GenreId == genreID))
			.Where(movie => movie.Projections.Any(p => p.CityId == cityID))
			.Select(movie => movie.Name).ToList();

			return movies;
		}

	}
}
