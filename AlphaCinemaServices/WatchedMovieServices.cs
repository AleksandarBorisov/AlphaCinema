using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaData.UnitOfWork;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using System;
using System.Collections.Generic;
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

        public List<int> GetProjectionsIDsByUser(string userName)
        {
            var projectionsIDs = this.unitOfWork.WatchedMovies.All()
                .Where(watchedMovie => watchedMovie.User.Name == userName)
                .Select(watchedMovie => watchedMovie.ProjectionId)
                .ToList();

            return projectionsIDs;
        }

        public List<int> GetUsersIDsByMovie(string movieName)
        {
            var usersIDs = this.unitOfWork.WatchedMovies.All()
				.Where(watchedMovie => watchedMovie.Projection.Movie.Name == movieName)
                .Select(watchedMovie => watchedMovie.UserId)
                .ToList();

            return usersIDs;
        }

        public List<int> GetUsersIDsByProjection(int cityID, int movieID, int openHourID)
        {
            var usersIDs = this.unitOfWork.WatchedMovies.All()
				.Where(watchedMovie => 
                watchedMovie.Projection.CityId == cityID &&
                watchedMovie.Projection.MovieId == movieID &&
                watchedMovie.Projection.OpenHourId == openHourID)
                .Select(watchedMovie => watchedMovie.UserId)
                .ToList();

            return usersIDs;
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


		private WatchedMovie IfExist(int userID, int projectionID)
		{

			return this.unitOfWork.WatchedMovies.AllAndDeleted()
				.Where(watchedMovie => watchedMovie.UserId == userID
				&& watchedMovie.ProjectionId == projectionID)
				.FirstOrDefault();
		}

	}
}
