using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AlphaCinemaServices
{
    public class WatchedMovieServices : IWatchedMovieServices
    {
		private readonly IUnitOfWork unitOfWork;

		public WatchedMovieServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}

		public void AddNewWatchedMovie(int userID, int resevationID)
		{
			if (IfExist(userID, resevationID) != null)
			{
				throw new EntityAlreadyExistsException("You have already booked this projection");
			}
			else
			{
				var watchedMovie = new WatchedMovie()
				{
					UserId = userID,
					ProjectionId = resevationID
				};
				this.unitOfWork.WatchedMovies.Add(watchedMovie);
				this.unitOfWork.SaveChanges();
			}
		}

		public IEnumerable<Movie> GetWatchedMoviesByUserName(string userName)
		{
			var movies = this.unitOfWork.Movies.All()
				.SelectMany(m => m.Projections)
				.SelectMany(wm => wm.WatchedMovies)
				.Where(u => u.User.Name == userName)
				.Select(m => m.Projection.Movie)
				.ToList();
			return movies;
		}

		private WatchedMovie IfExist(int userID, int projectionID)
		{
			return this.unitOfWork.WatchedMovies.AllAndDeleted()
				.Where(watchedMovie => watchedMovie.UserId == userID
				&& watchedMovie.ProjectionId == projectionID)
				.FirstOrDefault();
		}
	}
}
