using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaServices
{
    public class WatchedMovieServices : IWatchedMovieServices
    {
		private readonly IUnitOfWork unitOfWork;
		private WatchedMovie watchedMovie;

		public WatchedMovieServices(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}

		public void AddNewWatchedMovie(int userID, int resevationID)
		{
			watchedMovie = IfExist(userID, resevationID);
			if (watchedMovie != null)
			{
				throw new EntityAlreadyExistsException("You have already booked this projection");
			}
			else
			{
				watchedMovie = new WatchedMovie()
				{
					UserId = userID,
					ProjectionId = resevationID
				};
				this.unitOfWork.WatchedMovies.Add(watchedMovie);
				this.unitOfWork.SaveChanges();
			}
		}

		public IEnumerable<Movie> GetWatchedMoviesByUserName(string userName, int age)
		{
			var movies = this.unitOfWork.Movies.All()
				.SelectMany(m => m.Projections)
				.SelectMany(wm => wm.WatchedMovies)
				.Where(u => u.User.Name == userName)
				.Where(u => u.User.Age == age)
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
