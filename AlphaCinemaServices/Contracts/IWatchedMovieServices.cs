using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
        List<string> GetUsersIDsByMovie(string movieName);

        List<string> GetUsersIDsByProjection(string cityID, string movieID, string openHourID);

        List<string> GetProjectionsIDsByUser(string userName);
    }
}
