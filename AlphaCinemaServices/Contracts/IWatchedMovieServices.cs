using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
        List<string> GetUsersIDsByMovie(string movieName);

        List<string> GetUsersIDsByProjection(string cityID, string movieID, string openHourID);

        void AddNewWatchedMovie(string userId, string reservationId);

        List<string> GetProjectionsIDsByUser(string userName);
    }
}
