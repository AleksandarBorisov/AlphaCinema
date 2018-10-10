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

        public List<string> GetProjectionsIDsByUser(string userName)
        {
            var projectionsIDs = this.repository.All()
                .Where(watchedMovie => watchedMovie.User.Name == userName)
                .Select(watchedMovie => watchedMovie.ProjectionId.ToString())
                .ToList();

            return projectionsIDs;
        }

        public List<string> GetUsersIDsByMovie(string movieName)
        {
            var usersIDs = this.repository.All()
                .Where(watchedMovie => watchedMovie.Projection.Movie.Name == movieName)
                .Select(watchedMovie => watchedMovie.UserId.ToString())
                .ToList();



            return usersIDs;
        }

        public List<string> GetUsersIDsByProjection(string cityID, string movieID, string openHourID)
        {
            var usersIDs = this.repository.All()
                .Where(watchedMovie => 
                watchedMovie.Projection.CityId.ToString() == cityID &&
                watchedMovie.Projection.MovieId.ToString() == movieID &&
                watchedMovie.Projection.OpenHourId.ToString() == openHourID)
                .Select(watchedMovie => watchedMovie.UserId.ToString())
                .ToList();

            return usersIDs;
        }
    }
}
