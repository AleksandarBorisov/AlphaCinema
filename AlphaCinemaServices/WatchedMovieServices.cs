using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Repository;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaServices
{
    public class WatchedMovieServices : IWatchedMovieServices
    {
        private readonly IRepository<WatchedMovie> repository;

        public WatchedMovieServices(IRepository<WatchedMovie> repository)
        {
            this.repository = repository;
        }

        public List<int> GetProjectionsIDsByUser(string userName)
        {
            var projectionsIDs = this.repository.All()
                .Where(watchedMovie => watchedMovie.User.Name == userName)
                .Select(watchedMovie => watchedMovie.ProjectionId)
                .ToList();

            return projectionsIDs;
        }

        public List<int> GetUsersIDsByMovie(string movieName)
        {
            var usersIDs = this.repository.All()
                .Where(watchedMovie => watchedMovie.Projection.Movie.Name == movieName)
                .Select(watchedMovie => watchedMovie.UserId)
                .ToList();

            return usersIDs;
        }

        public List<int> GetUsersIDsByProjection(int cityID, int movieID, int openHourID)
        {
            var usersIDs = this.repository.All()
                .Where(watchedMovie => 
                watchedMovie.Projection.CityId == cityID &&
                watchedMovie.Projection.MovieId == movieID &&
                watchedMovie.Projection.OpenHourId == openHourID)
                .Select(watchedMovie => watchedMovie.UserId)
                .ToList();

            return usersIDs;
        }
    }
}
