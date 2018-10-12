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

        //public List<int> GetProjectionsIDsByUser(string userName)
        //{
        //    var projectionsIDs = this.unitOfWork.WatchedMovies.All()
        //        .Where(watchedMovie => watchedMovie.User.Name == userName)
        //        .Select(watchedMovie => watchedMovie.ProjectionId)
        //        .ToList();

        //    return projectionsIDs;
        //}

        //public List<int> GetUsersIDsByMovie(string movieName)
        //{
        //    var usersIDs = this.unitOfWork.WatchedMovies.All()
        //        .Where(watchedMovie => watchedMovie.Projection.Movie.Name == movieName)
        //        .Select(watchedMovie => watchedMovie.UserId)
        //        .ToList();

        //    return usersIDs;
        //}

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
                    UserId = int.Parse(userId),
                    ProjectionId = int.Parse(reservationId)
                };
                this.unitOfWork.WatchedMovies.Add(watchedMovie);
                this.unitOfWork.SaveChanges();
            }
        }

        private WatchedMovie IfExist(string userIdAsString, string projectionIdAsString)
        {
            int userId = int.Parse(userIdAsString);
            int projectionId = int.Parse(projectionIdAsString);
            return this.unitOfWork.WatchedMovies.AllAndDeleted()
                .Where(watchedMovie => watchedMovie.UserId == userId && watchedMovie.ProjectionId == projectionId)
                .FirstOrDefault();
        }
    }
}
