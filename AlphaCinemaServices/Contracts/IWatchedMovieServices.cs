﻿using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaServices.Contracts
{
    public interface IWatchedMovieServices
    {
        List<int> GetUsersIDsByMovie(string movieName);

        List<int> GetUsersIDsByProjection(int cityID, int movieID, int openHourID);

        List<int> GetProjectionsIDsByUser(string userName);
		void AddNewWatchedMovie(int userID, int resevationID);
		IEnumerable<Movie> GetWatchedMoviesByUserName(string userName);

	}
}
