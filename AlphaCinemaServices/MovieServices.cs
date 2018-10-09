using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
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
	}
}
