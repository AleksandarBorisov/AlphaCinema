using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
        //List<int> GetUsersIDsByMovie(string movieName);

        List<int> GetUsersIDsByProjection(int cityID, int movieID, int openHourID);

        void AddNewWatchedMovie(string userId, string reservationId);

        //List<int> GetProjectionsIDsByUser(string userName);
    }
}
