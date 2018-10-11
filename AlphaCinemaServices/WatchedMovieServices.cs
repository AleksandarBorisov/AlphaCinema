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

        public List<string> GetProjectionsIDsByUser(string userName)
        {
            var projectionsIDs = this.unitOfWork.WatchedMovies.All()
                .Where(watchedMovie => watchedMovie.User.Name == userName)
                .Select(watchedMovie => watchedMovie.ProjectionId.ToString())
                .ToList();

            return projectionsIDs;
        }

        public List<string> GetUsersIDsByMovie(string movieName)
        {
            var usersIDs = this.unitOfWork.WatchedMovies.All()
                .Where(watchedMovie => watchedMovie.Projection.Movie.Name == movieName)
                .Select(watchedMovie => watchedMovie.UserId.ToString())
                .ToList();

            return usersIDs;
        }

        public List<string> GetUsersIDsByProjection(string cityID, string movieID, string openHourID)
        {
            var usersIDs = this.unitOfWork.WatchedMovies.All()
                .Where(watchedMovie => 
                watchedMovie.Projection.CityId.ToString() == cityID &&
                watchedMovie.Projection.MovieId.ToString() == movieID &&
                watchedMovie.Projection.OpenHourId.ToString() == openHourID)
                .Select(watchedMovie => watchedMovie.UserId.ToString())
                .ToList();

            return usersIDs;
        }

        public void AddNewWatchedMovie(string userId, string reservationId)
        {
            if (IfExist(userId, reservationId) != null)
            {
                throw new EntityAlreadyExistsException("You have already booked this projection");
            }
            else
            {
                var watchedMovie = new WatchedMovie()
                {
                    UserId = Guid.Parse(userId),
                    ProjectionId = Guid.Parse(reservationId)
                };
                this.unitOfWork.WatchedMovies.Add(watchedMovie);
            }
        }

        private WatchedMovie IfExist(string userId, string projectionId)
        {
            return this.unitOfWork.WatchedMovies.AllAndDeleted()
                .Where(watchedMovie => watchedMovie.UserId.ToString() == userId && watchedMovie.ProjectionId.ToString() == projectionId)
                .FirstOrDefault();
        }
    }
}
